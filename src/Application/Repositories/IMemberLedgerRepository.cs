using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IMemberLedgerRepository
    {
        Task<MemberLedger> Add(MemberLedger MemberLedger);
        Task<MemberLedger> Update(MemberLedger MemberLedger);
        Task Delete(MemberLedger MemberLedger);
        Task<MemberLedger> Get(Expression<Func<MemberLedger, bool>> expression);
        Task<IList<MemberLedger>> GetAll(Expression<Func<MemberLedger, bool>> expression);
    }
}
