using Application.Exceptions;
using Application.Paging;
using Application.Repositories;
using Domain.Constants;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetMemberReport
    {
        public record Query(Guid? Id, string? ChandaType, bool UsePaging): PageRequest, IRequest<MemberReport>;
        
        public record MemberReport
        {
            public decimal TotalAmountSummary { get; set; }
            public int NoOfMonthsPaid { get; set; }
            
            //public int PaidByNoOfMembers { get; set; }
            public PaginatedList<MemberReportResponse> ReportResponses { get; set;} = new PaginatedList<MemberReportResponse>();
        }

        public record MemberReportResponse
        {
            public Guid MemberId { get; set; }
            public string Name { get; set; } = default!;
            public string ChandaNo { get; set; } = default!;
            public decimal SubTotal { get; set; }
            public MonthOfTheYear Month { get; set; } = default!;
            public int Year { get; set; }
            public string ReferenceNo { get; set; } = default!;
            public string? RecieptNo { get; set; }
            public DateTime PaymentDate { get; set; }
            public List<Item> Items { get; set; } = new List<Item>();
        }

        public record Item(string ChandaType, decimal amount);

        public class Handler : IRequestHandler<Query, MemberReport>
        {
            private readonly ICurrentUser _currentUser;
            private readonly IMemberRepository _memberRepository;
            private readonly IInvoiceItemRepository _invoiceItemRepository;

            public Handler(IInvoiceItemRepository invoiceItemRepository, ICurrentUser currentUser, IMemberRepository memberRepository)
            {
                _currentUser = currentUser;
                _memberRepository = memberRepository;
                _invoiceItemRepository = invoiceItemRepository;
            }

            public async Task<MemberReport> Handle(Query request, CancellationToken cancellationToken)
            {
                var initiator = _currentUser.GetMemberDetails();
                if (initiator == null)
                {
                    throw new NotFoundException($"Please login to view report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                Guid? id = request.Id;
                if(id == null)
                {
                    id = initiator.Id;
                }
                else
                {
                    var roles = initiator.Roles.Split(",");
                    if (initiator.Id != request.Id
                        && !roles.Any(r => r.Equals(Roles.Admin))
                        && !roles.Any(r => r.Equals(Roles.Amir))
                        && !roles.Any(r => r.Equals(Roles.ActingAmir))
                        && !roles.Any(r => r.Equals(Roles.NaibAmir))
                        && !roles.Any(r => r.Equals(Roles.NationaGenSec))
                        && !roles.Any(r => r.Equals(Roles.NationlFinSec))
                        && !roles.Any(r => r.Equals(Roles.CP))
                        && !roles.Any(r => r.Equals(Roles.CircuitFinSec))
                        && !roles.Any(r => r.Equals(Roles.JamaatPresident))
                        && !roles.Any(r => r.Equals(Roles.JamaatFinSec)))
                    {
                        throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                    }

                    var member = _memberRepository.GetMember(request.Id);
                    if (member != null)
                    {
                        if ( (roles.Any(r => r.Equals(Roles.JamaatPresident)) || roles.Any(r => r.Equals(Roles.JamaatFinSec)))  && member.JamaatId != initiator.JamaatId)
                        {
                            throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                        }

                        if ( (roles.Any(r => r.Equals(Roles.CP)) || roles.Any(r => r.Equals(Roles.CircuitFinSec))) && member.Jamaat.CircuitId != initiator.CircuitId )
                        {
                            throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                        }
                    }
                    else
                    {
                        throw new NotFoundException($"Member not found.", ExceptionCodes.MemberNotFound.ToString(), 404);
                    }
                    
                }

                return await _invoiceItemRepository.GetMemberReportAsync(id, request.ChandaType, request, request.UsePaging);
            }
        }
    }
}
