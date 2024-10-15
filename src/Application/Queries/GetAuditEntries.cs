using Application.Paging;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetAuditEntries
    {
        public record Query(string? Id): PageRequest, IRequest<PaginatedList<AuditEntryResponse>>;

        public record AuditEntryResponse(Guid Id, string EntityName, string Action, string ActionName, string UserId, string UserName, DateTime Timestamp);

        public class Handler : IRequestHandler<Query, PaginatedList<AuditEntryResponse>>
        {
            private readonly IAuditEntryRepository _auditRepo;

            public Handler(IAuditEntryRepository auditRepo)
            {
                _auditRepo = auditRepo;
            }

            public async Task<PaginatedList<AuditEntryResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _auditRepo.GetAuditEntriesAsync(request.Id, request);
            }
        }

    }
}
