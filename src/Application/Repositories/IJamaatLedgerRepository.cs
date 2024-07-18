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
        Task<JamaatLedger> Add(JamaatLedger JamaatLedger);
        Task<JamaatLedger> Update(JamaatLedger JamaatLedger);
        Task Delete(JamaatLedger JamaatLedger);
        Task<JamaatLedger> Get(Expression<Func<JamaatLedger, bool>> expression);
        Task<IList<JamaatLedger>> GetAll(Expression<Func<JamaatLedger, bool>> expression);
    }
}
