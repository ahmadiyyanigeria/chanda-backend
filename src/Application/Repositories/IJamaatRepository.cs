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
        Task<int> Add(Jamaat jamaat);
        Task<int> Update(Jamaat jamaat);
        Task<int> Delete(Jamaat jamaat);
        Task<Jamaat> Get(Expression<Func<Jamaat, bool>> expression);
        Task<IList<Jamaat>> GetAll(Expression<Func<Jamaat, bool>> expression);
    
    }
}
