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
        Task<Jamaat> AddAsync(Jamaat jamaat);
        Task<Jamaat> Update(Jamaat jamaat);
        Task<Jamaat?> Get(Expression<Func<Jamaat, bool>> expression);
        Task<PaginatedList<Jamaat>> GetAllAsync(PageRequest pageRequest, Expression<Func<Jamaat, bool>> expression, bool usePaging);
       


    }
}
