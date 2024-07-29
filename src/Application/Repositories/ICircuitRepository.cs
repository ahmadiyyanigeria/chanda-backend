using Domain.Entities;
using Application.Paging;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface ICircuitRepository
    {
        Task<Circuit> AddAsync(Circuit circuit);
        Task<Circuit> UpdateAsync(Circuit circuit);
        Task<Circuit?> GetAsync(Expression<Func<Circuit, bool>> expression);
        Task<PaginatedList<Circuit>> GetAllAsync(PageRequest pageRequest, Expression<Func<Circuit, bool>> expression, bool usePaging);
    }
}