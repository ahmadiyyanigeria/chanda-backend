using Application.Paging;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> AddAsync(Invoice invoice);
        Task<Invoice> UpdateAsync(Invoice invoice);
        Invoice Update(Invoice invoice);
        Task<Invoice?> GetAsync(Expression<Func<Invoice, bool>> expression);
        Task<PaginatedList<Invoice>> GetAllAsync(PageRequest request, Guid? jamaatId, bool usePaging);
    }
}
