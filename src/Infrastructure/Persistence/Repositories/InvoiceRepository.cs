﻿using Application.Paging;
using Application.Repositories;
using Domain.Entities;
using Infrastructure.Mapping.Extensions;
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
                ? _context.Invoices.Include(i => i.Jamaat).Where(i => i.JamaatId == jamaatId)
                : _context.Invoices;

           
            if (request.IsDescending)
            {
                query = query.OrderByDescending(i => i.CreatedOn);
            }

            var totalCount = await query.CountAsync();
            
            if (usePaging)
            {
                var offset = (request.Page - 1) * request.PageSize;

                var result = await query.ToListAsync();
                result = (string.IsNullOrWhiteSpace(request.Keyword)) ? result.Skip(offset).Take(request.PageSize).ToList() : result.SearchByKeyword(request.Keyword).Skip(offset).Take(request.PageSize).ToList();

                return result.ToPaginatedList(totalCount, request.Page, request.PageSize);
            }
            else
            {
                var result = await query.ToListAsync();
                result = (string.IsNullOrWhiteSpace(request.Keyword)) ? result: [.. result.SearchByKeyword(request.Keyword)];
                return result.ToPaginatedList(totalCount, 1, totalCount);
            }
        }

        public async Task<Invoice?> GetAsync(Expression<Func<Invoice, bool>> expression)
        {
            var invoice = await _context.Invoices.Where(expression).Include(i => i.Jamaat)
                .Include(i => i.InvoiceItems).ThenInclude(ii => ii.Member).ThenInclude(m => m.Jamaat).ThenInclude(j => j.JamaatLedger)
                .Include(i => i.InvoiceItems).ThenInclude(ii => ii.Member).ThenInclude(m => m.MemberLedger)
                .Include(i => i.InvoiceItems).ThenInclude(ii => ii.ChandaItems).ThenInclude(ci => ci.ChandaType)
                .Include(i => i.Payments).SingleOrDefaultAsync();

            return invoice;
        }

        public Invoice? Get(Expression<Func<Invoice, bool>> expression)
        {
            var invoice = _context.Invoices.Where(expression).SingleOrDefault();
            return invoice;
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
