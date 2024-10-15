using Application.Paging;
using static Application.Queries.GetAuditEntries;

namespace Application.Repositories
{
    public interface IAuditEntryRepository
    {
        Task<PaginatedList<AuditEntryResponse>> GetAuditEntriesAsync(string? id, PageRequest request);
    }
}
