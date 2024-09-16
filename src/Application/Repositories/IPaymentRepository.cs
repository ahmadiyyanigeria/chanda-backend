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
        Payment Update(Payment payment);
        Task<Payment?> GetByIdAsync(Guid id);
        Task<Payment?> GetAsync(Expression<Func<Payment, bool>> expression);
        Task<PaginatedList<Payment>> GetAllAsync(PageRequest pageRequest, bool usePaging);
    }
}
