using Domain.Entities;
using Application.Paging;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CircuitRepository(AppDbContext _context) : ICircuitRepository
    {
        public async Task<Circuit> AddAsync(Circuit circuit)
        {
            await _context.Circuits.AddAsync(circuit);
            return circuit;
        }

        public async Task<Circuit?> GetAsync(Expression<Func<Circuit, bool>> expression)
        {
            return await _context.Circuits.SingleOrDefaultAsync(expression);
        }

        public async Task<PaginatedList<Circuit>> GetAllAsync(PageRequest pageRequest, Expression<Func<Circuit, bool>> expression, bool usePaging)
        {
            var query =  _context.Circuits.Where(expression);
            if (!string.IsNullOrEmpty(pageRequest.Keyword))
            {
                query = query.Where(m => m.Name.Contains(pageRequest.Keyword, StringComparison.OrdinalIgnoreCase)).Order();
            }

            if (pageRequest.IsDescending)
            {
                query = query.OrderDescending();
            }

            var totalCount = await query.CountAsync();

            if (usePaging)
            {
                var offset = (pageRequest.Page - 1) * pageRequest.PageSize;
                var result = await query.Skip(offset).Take(pageRequest.PageSize).ToListAsync();
                return result.ToPaginatedList(totalCount, pageRequest.Page, pageRequest.PageSize);
            }
            else
            {
                var result = await query.ToListAsync();
                return result.ToPaginatedList(totalCount, 1, totalCount);
            }
        }

        public Task<Circuit> UpdateAsync(Circuit circuit)
        {
            _context.Entry(circuit).State = EntityState.Modified;
            return Task.FromResult(circuit);
        }
    }
}