using Application.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> AddAsync(Payment payment);
        Task<Payment> UpdateAsync(Payment payment);
        Task<Payment> DeleteAsync(Guid paymentId, Payment payment);
        Task<Payment> GetByIdAsync(Guid id);
        Task<Payment> GetAsync(Expression<Func<Payment, bool>> expression);
        Task<PaginatedList<Payment>> GetAllAsync(PageRequest pageRequest, Expression<Func<Payment, bool>> expression, bool usePaging);
    }
}
