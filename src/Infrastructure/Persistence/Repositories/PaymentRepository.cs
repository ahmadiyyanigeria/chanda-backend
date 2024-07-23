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
    public class PaymentRepository(AppDbContext _context) : IPaymentRepository
    {
        public async Task<Payment> AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return payment;
        }


        public async Task<PaginatedList<Payment>> GetAllAsync(PageRequest pageRequest, Expression<Func<Payment, bool>> expression, bool usePaging)
        {
            var query = _context.Payments.Where(expression);
            

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

        public async  Task<Payment> GetAsync(Expression<Func<Payment, bool>> expression)
        {
            return await _context.Payments.SingleOrDefaultAsync(expression);
        }

        public async Task<Payment> GetByIdAsync(Guid id)
        {
            var payment = await _context.Payments.SingleOrDefaultAsync(p => p.Id == id);
            return payment;
        }

        public async Task<Payment> UpdateAsync(Payment payment)
        {
            _context.Payments.Remove(payment);
            return payment;
        }
    }
}
