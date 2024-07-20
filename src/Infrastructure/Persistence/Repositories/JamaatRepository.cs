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
    public class JamaatRepository(AppDbContext _context) : IJamaatRepository
    {
        public async Task<Jamaat> Add(Jamaat jamaat)
        {
            await _context.Jamaats.AddAsync(jamaat);
            return jamaat;
        }

        public async Task Delete(Jamaat jamaat)
        {
            _context.Jamaats.Remove(jamaat);
        }

        public async Task<Jamaat> Get(Expression<Func<Jamaat, bool>> expression)
        {
            return await _context.Jamaats.SingleOrDefaultAsync(expression);
        }

        public async Task<PaginatedList<Jamaat>> GetAll(PageRequest pageRequest, Expression<Func<Jamaat, bool>> expression, bool usePaging)
        {
            var query =  _context.Jamaats.Where(expression);
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

        public async Task<Jamaat> Update(Jamaat jamaat)
        {
            _context.Jamaats.Update(jamaat);
            return jamaat;
        }
    }
}