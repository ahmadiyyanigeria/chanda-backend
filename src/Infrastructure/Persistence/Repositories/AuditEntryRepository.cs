using Application.Paging;
using Application.Repositories;
using Infrastructure.Mapping.Extensions;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Queries.GetAuditEntries;

namespace Infrastructure.Persistence.Repositories
{
    public class AuditEntryRepository : IAuditEntryRepository
    {
        private readonly AppDbContext _context;

        public AuditEntryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedList<AuditEntryResponse>> GetAuditEntriesAsync(string? id, PageRequest request)
        {
            var query = id == null
                ? _context.AuditEntries
                : _context.AuditEntries.Where(a => a.UserId == id);

            if (request.IsDescending)
            {
                query = query.OrderByDescending(e => e.Timestamp);
            }

            var totalCount = await query.CountAsync();

            var offset = (request.Page - 1) * request.PageSize;

            var result = await query.ToListAsync();
            result = (string.IsNullOrWhiteSpace(request.Keyword)) ? result.Skip(offset).Take(request.PageSize).ToList() : result.SearchByKeyword(request.Keyword).Skip(offset).Take(request.PageSize).ToList();

            var response = result.ToPaginatedList(totalCount, request.Page, request.PageSize);            
            return response.Adapt<PaginatedList<AuditEntryResponse>>();
        }
    }
}
