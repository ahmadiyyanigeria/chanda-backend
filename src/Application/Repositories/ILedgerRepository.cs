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
    public interface ILedgerRepository
    {
        Task<Ledger> AddAsync(Ledger ledger);
        Task<Ledger?> GetAsync(Guid id);
        Task<PaginatedList<Ledger>> GetAllAsync(PageRequest request, Expression<Func<Ledger, bool>> expression, bool usePaging);
    }
}
