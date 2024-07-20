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
    public interface IJamaatRepository
    {
        Task<Jamaat> Add(Jamaat jamaat);
        Task<Jamaat> Update(Jamaat jamaat);
        Task Delete(Jamaat jamaat);
        Task<Jamaat> Get(Expression<Func<Jamaat, bool>> expression);
        Task<PaginatedList<Jamaat>> GetAll(PageRequest pageRequest, Expression<Func<Jamaat, bool>> expression, bool usePaging);


    }
}
