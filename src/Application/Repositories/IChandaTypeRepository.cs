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
    public interface IChandaTypeRepository
    {
        Task<ChandaType> AddAsync(ChandaType chandaType);
        Task<ChandaType> UpdateAsync(ChandaType chandaType);
        Task<ChandaType> GetByIdAsync(Guid id);
        Task<ChandaType> GetAsync(Expression<Func<ChandaType, bool>> predicate);
        Task<PaginatedList<ChandaType>> GetAllAsync(PageRequest pageRequest, Expression<Func<ChandaType, bool>> predicate, bool usePaging);
    }
}
