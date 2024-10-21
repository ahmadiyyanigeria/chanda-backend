using Application.Exceptions;
using Application.Paging;
using Application.Repositories;
using Domain.Constants;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;
using static Application.Queries.GetJamaatReport;

namespace Application.Queries
{
    public class GetCircuitReport
    {
        public record Query(Guid CircuitId, string? ChandaType, bool UsePaging) : PageRequest, IRequest<CircuitReport>;

        public record CircuitReport
        {
            public decimal TotalAmountSummary { get; set; }
            public int PaidByNoOfJamaats { get; set; }
            public PaginatedList<CircuitReportResponse> ReportResponses { get; set; } = new PaginatedList<CircuitReportResponse>();
        }

        public record CircuitReportResponse
        {
            public Guid CircuitId { get; set; }
            public string CircuitName { get; set; } = default!;
            public MonthOfTheYear Month { get; set; }
            public int Year { get; set; }
            public decimal MonthAmount { get; set; }
            public int NoOfJamaats { get; set; }
            public List<ItemObject> Items { get; set; } = new List<ItemObject>();
        }

        public class Handler : IRequestHandler<Query, CircuitReport>
        {
            private readonly ICurrentUser _currentUser;
            private readonly ICircuitRepository _circuitRepository;
            private readonly IInvoiceItemRepository _invoiceItemRepository;

            public Handler(ICurrentUser currentUser, ICircuitRepository circuitRepository, IInvoiceItemRepository invoiceItemRepository)
            {
                _currentUser = currentUser;
                _circuitRepository = circuitRepository;
                _invoiceItemRepository = invoiceItemRepository;
            }

            public async Task<CircuitReport> Handle(Query request, CancellationToken cancellationToken)
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
                    && !roles.Any(r => r.Equals(Roles.CircuitFinSec)))
                {
                    throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var circuit = await _circuitRepository.GetAsync(c => c.Id == request.CircuitId);
                if (circuit == null)
                {
                    throw new NotFoundException($"Circuit not found.", ExceptionCodes.MemberNotFound.ToString(), 404);
                }

                if ((roles.Any(r => r.Equals(Roles.CP)) || roles.Any(r => r.Equals(Roles.CircuitFinSec))) && circuit.Id != initiator.CircuitId)
                {
                    throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                return await _invoiceItemRepository.GetCircuitReportAsync(request.CircuitId, request.ChandaType, request, request.UsePaging);
            }
        }
    }
}
