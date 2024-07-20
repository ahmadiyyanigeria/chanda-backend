using Domain.Entities;
using Application.Paging;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface ICircuitRepository
    {
        Task<Circuit> Add(Circuit circuit);
        Task<Circuit> Update(Circuit circuit);
        Task<Circuit> Get(Expression<Func<Circuit, bool>> expression);
        Task<PaginatedList<Circuit>> GetAll(PageRequest pageRequest, Expression<Func<Circuit, bool>> expression, bool usePaging);
    }
}