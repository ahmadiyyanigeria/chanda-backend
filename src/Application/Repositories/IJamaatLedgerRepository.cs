using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IJamaatLedgerRepository
    {
        Task<int> Add(JamaatLedger JamaatLedger);
        Task<int> Update(JamaatLedger JamaatLedger);
        Task<int> Delete(JamaatLedger JamaatLedger);
        Task<JamaatLedger> Get(Expression<Func<JamaatLedger, bool>> expression);
        Task<IList<JamaatLedger>> GetAll(Expression<Func<JamaatLedger, bool>> expression);
    }
}
