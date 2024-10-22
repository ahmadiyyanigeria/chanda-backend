using Application.Exceptions;
using Application.Paging;
using Application.Repositories;
using Domain.Constants;
using Domain.Exceptions;
using MediatR;
using static Application.Queries.GetCircuitReport;

namespace Application.Queries
{
    public class GetOverrallSummary
    {
        public record Query(string? ChandaType, bool UsePaging): PageRequest, IRequest<OverrallReport>;

        public record OverrallReport
        {
            public decimal TotalAmountSummary { get; set; }
            public int PaidByNoOfCircuits { get; set; }
            public PaginatedList<CircuitReportResponse> ReportResponses { get; set; } = new PaginatedList<CircuitReportResponse>();
        }

        public class Handler : IRequestHandler<Query, OverrallReport>
        {
            private readonly ICurrentUser _currentUser;
            private readonly IChandaTypeRepository _chandaTypeRepository;
            private readonly IInvoiceItemRepository _invoiceItemRepository;

            public Handler(ICurrentUser currentUser, IInvoiceItemRepository invoiceItemRepository, IChandaTypeRepository chandaTypeRepository)
            {
                _currentUser = currentUser;
                _invoiceItemRepository = invoiceItemRepository;
                _chandaTypeRepository = chandaTypeRepository;
            }

            public async Task<OverrallReport> Handle(Query request, CancellationToken cancellationToken)
            {
                var initiator = _currentUser.GetMemberDetails();
                if (initiator == null)
                {
                    throw new NotFoundException($"Please login to view report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                if (!string.IsNullOrEmpty(request.ChandaType))
                {
                    if (!_chandaTypeRepository.Any(ct => ct.Name == request.ChandaType))
                    {
                        throw new NotFoundException($"ChandaType not exist.", ExceptionCodes.MemberNotFound.ToString(), 404);
                    }
                }

                var roles = initiator.Roles.Split(",");
                if (!roles.Any(r => r.Equals(Roles.Admin))
                    && !roles.Any(r => r.Equals(Roles.Amir))
                    && !roles.Any(r => r.Equals(Roles.ActingAmir))
                    && !roles.Any(r => r.Equals(Roles.NaibAmir))
                    && !roles.Any(r => r.Equals(Roles.NationaGenSec))
                    && !roles.Any(r => r.Equals(Roles.NationlFinSec)))
                {
                    throw new NotFoundException($"You do not have permission to view this report.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                return await _invoiceItemRepository.GetOverrallReportAsync(request.ChandaType, request, request.UsePaging);
            }
        }
    }
}
