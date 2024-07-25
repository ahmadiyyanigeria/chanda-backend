using Application.Paging;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class LedgerRepository : ILedgerRepository
    {
        private readonly AppDbContext _context;

        public LedgerRepository(AppDbContext context) 
        { 
            _context = context;
        }

        public async Task<Ledger> AddAsync(Ledger ledger)
        {
            await _context.AddAsync(ledger);
            return ledger;
        }

        public async Task<PaginatedList<Ledger>> GetAllAsync(PageRequest request, Expression<Func<Ledger, bool>> expression, bool usePaging)
        {
            var query = _context.Ledgers.Include(l => l.ChandaType).Where(expression).OrderBy(l => l.CreatedOn);

            if (request.IsDescending)
            {
                query = query.OrderByDescending(i => i.CreatedOn);
            }

            var totalCount = await query.CountAsync();

            if (usePaging)
            {
                var offset = (request.Page - 1) * request.PageSize;
                var result = await query.Skip(offset).Take(request.PageSize).ToListAsync();
                return result.ToPaginatedList(totalCount, request.Page, request.PageSize);
            }
            else
            {
                var result = await query.ToListAsync();
                return result.ToPaginatedList(totalCount, 1, totalCount);
            }
        }

        public async Task<Ledger?> GetAsync(Guid id)
        {
            return await _context.Ledgers.Include(l => l.ChandaType).SingleOrDefaultAsync(l => l.Id == id);
        }
    }
}
