using Application.Exceptions;
using Application.Paging;
using Application.Repositories;
using Domain.Constants;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;

namespace Application.Queries
{
    public class GetJamaatReport
    {
        public record Query(Guid JamaatId, string? ChandaType, bool UsePaging) : PageRequest, IRequest<JamaatReport>;

        public record JamaatReport
        {
            public decimal TotalAmountSummary { get; set; }
            public int PaidByNoOfMembers { get; set; }
            public PaginatedList<JamaatReportResponse> ReportResponses { get; set; } = new PaginatedList<JamaatReportResponse>();
        }

        public record JamaatReportResponse
        {
            public Guid JamaatId { get; set; }
            public string JamaatName { get; set; } = default!;
            public MonthOfTheYear Month { get; set; }
            public int Year { get; set; }
            public decimal MonthAmount { get; set; }
            public int NoOfMembers { get; set; }
            public List<ItemObject> Items { get; set; } = new List<ItemObject>();
        }

        public class ItemObject
        {
            public string ChandaType { get; set; } = default!;
            public decimal Amount { get; set; }
        }

        public class Handler : IRequestHandler<Query, JamaatReport>
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

            public async Task<JamaatReport> Handle(Query request, CancellationToken cancellationToken)
            {
                var initiator = _currentUser.GetMemberDetails();
                if (initiator == null)
                {
                    throw new NotFoundException($"Please login to view report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var roles = initiator.Roles.Split(",");
                if ( !roles.Any(r => r.Equals(Roles.Admin))
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
                return await _invoiceItemRepository.GetJamaatReportAsync(request.JamaatId, request.ChandaType, request, request.UsePaging);
            }
        }
    }
}
