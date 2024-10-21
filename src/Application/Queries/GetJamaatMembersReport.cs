using Application.Exceptions;
using Application.Paging;
using Application.Repositories;
using Domain.Constants;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;
using static Application.Queries.GetMemberReport;

namespace Application.Queries
{
    public class GetJamaatMembersReport
    {
        public record Query(Guid JamaatId, string? ChandaType, bool UsePaging) : PageRequest, IRequest<JamaatMembersReport>;

        public record JamaatMembersReport
        {
            public decimal TotalAmountSummary { get; set; }
            public int PaidByNoOfMembers { get; set; }
            public PaginatedList<MemberReportResponse> ReportResponses { get; set; } = new PaginatedList<MemberReportResponse>();
        }

        public class Handler : IRequestHandler<Query, JamaatMembersReport>
        {
            private readonly ICurrentUser _currentUser;
            private readonly IJamaatRepository _jamaatRepository;
            private readonly IInvoiceItemRepository _invoiceItemRepository;

            public Handler(IInvoiceItemRepository invoiceItemRepository, ICurrentUser currentUser, IJamaatRepository jamaatRepository)
            {
                _currentUser = currentUser;
                _jamaatRepository = jamaatRepository;
                _invoiceItemRepository = invoiceItemRepository;
            }

            public async Task<JamaatMembersReport> Handle(Query request, CancellationToken cancellationToken)
            {
                var initiator = _currentUser.GetMemberDetails();
                if (initiator == null)
                {
                    throw new NotFoundException($"Please login to view report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var roles = initiator.Roles.Split(",");
                if (!roles.Any(r => r.Equals(Roles.Admin))
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

                if ((roles.Any(r => r.Equals(Roles.JamaatPresident)) || roles.Any(r => r.Equals(Roles.JamaatFinSec))) && request.JamaatId != initiator.JamaatId)
                {
                    throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var jamaat = await _jamaatRepository.GetAsync(j => j.Id == request.JamaatId);
                if (jamaat == null)
                {
                    throw new NotFoundException($"Jamaat not found.", ExceptionCodes.MemberNotFound.ToString(), 404);
                }

                if ((roles.Any(r => r.Equals(Roles.CP)) || roles.Any(r => r.Equals(Roles.CircuitFinSec))) && jamaat.CircuitId != initiator.CircuitId)
                {
                    throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                return await _invoiceItemRepository.GetJamaatMembersReportAsync(request.JamaatId, request.ChandaType, request);
            }
        }
    }
}
