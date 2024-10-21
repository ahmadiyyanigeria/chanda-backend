using Application.Exceptions;
using Application.Paging;
using Application.Repositories;
using Domain.Constants;
using Domain.Exceptions;
using MediatR;
using static Application.Queries.GetJamaatReport;

namespace Application.Queries
{
    public class GetCircuitJamaatsReport
    {
        public record Query(Guid CircuitId, string? ChandaType, bool UsePaging) : PageRequest, IRequest<CircuitJamaatsReport>;

        public record CircuitJamaatsReport
        {
            public decimal TotalAmountSummary { get; set; }
            public int PaidByNoOfJamaats { get; set; }
            public PaginatedList<JamaatReportResponse> ReportResponses { get; set; } = new PaginatedList<JamaatReportResponse>();
        }

        public class Handler : IRequestHandler<Query, CircuitJamaatsReport>
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

            public async Task<CircuitJamaatsReport> Handle(Query request, CancellationToken cancellationToken)
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

                return await _invoiceItemRepository.GetCircuitJamaatsReportAsync(request.CircuitId, request.ChandaType, request, request.UsePaging);
            }
        }
    }
}
