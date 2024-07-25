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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            return invoice;
        }

        public async Task<PaginatedList<Invoice>> GetAllAsync(PageRequest request, Guid? jamaatId, bool usePaging)
        {
            var query = jamaatId != null
                ? _context.Invoices.Include(i => i.Jamaat).Where(i => i.JamaatId == jamaatId).OrderBy(i => i.CreatedOn)
                : _context.Invoices.Include(i => i.Jamaat).OrderBy(i => i.CreatedOn);

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

        public async Task<Invoice?> GetAsync(Expression<Func<Invoice, bool>> expression)
        {
            return await _context.Invoices.Include(i => i.Jamaat).Include(i => i.InvoiceItems).ThenInclude(ii => ii.ChandaItems)
                .Include(i => i.Payments).SingleOrDefaultAsync(expression);
        }

        public Invoice Update(Invoice invoice)
        {
            _context.Update(invoice);
            return invoice;
        }

        public Task<Invoice> UpdateAsync(Invoice invoice)
        {
            _context.Entry(invoice).State = EntityState.Modified;
            return Task.FromResult(invoice);
        }
    }
}
