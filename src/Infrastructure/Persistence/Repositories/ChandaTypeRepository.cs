using Application.Paging;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ChandaTypeRepository(AppDbContext _context) : IChandaTypeRepository
    {
        public async Task<ChandaType> AddAsync(ChandaType chandaType)
        {
            await _context.ChandaTypes.AddAsync(chandaType);
            return chandaType;
        }

        public async Task<ChandaType> DeleteAsync(Guid chandaTypeId, ChandaType chandaType)
        {
            var delChanda = await GetByIdAsync(chandaTypeId);
            if (delChanda != null) 
            {
                delChanda.IsDeleted = true;
            }
            _context.ChandaTypes.Update(delChanda);
            await _context.SaveChangesAsync();
            return delChanda;


        }

        public async Task<PaginatedList<ChandaType>> GetAllAsync(PageRequest pageRequest, Expression<Func<ChandaType, bool>> predicate, bool usePaging)
        {
            var query = _context.ChandaTypes.Where(predicate);
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

        public async Task<ChandaType> GetAsync(Expression<Func<ChandaType, bool>> predicate)
        {
            return await _context.ChandaTypes.SingleOrDefaultAsync(predicate);
        }

        public async Task<ChandaType> GetByIdAsync(Guid id)
        {
            var chandaType = await _context.ChandaTypes.SingleOrDefaultAsync(ct => ct.Id == id);
            return chandaType;
        }

        public async Task<ChandaType> UpdateAsync(ChandaType chandaType)
        {
            _context.ChandaTypes.Remove(chandaType);
            return chandaType;
        }
    }
}
