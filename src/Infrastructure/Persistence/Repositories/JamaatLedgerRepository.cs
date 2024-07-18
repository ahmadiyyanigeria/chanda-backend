using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class JamaatLedgerRepository(AppDbContext _context) : IJamaatLedgerRepository
    {
        public async Task<JamaatLedger> Add(JamaatLedger jamaatLedger)
        {
            await _context.JamaatLedgers.AddAsync(jamaatLedger);
            return jamaatLedger;
        }

        public async Task Delete(JamaatLedger jamaatLedger)
        {
            _context.JamaatLedgers.Remove(jamaatLedger);
        }

        public async Task<JamaatLedger> Get(Expression<Func<JamaatLedger, bool>> expression)
        {
            return await _context.JamaatLedgers.SingleOrDefaultAsync(expression);
        }

        public async Task<IList<JamaatLedger>> GetAll(Expression<Func<JamaatLedger, bool>> expression)
        {
            return await _context.JamaatLedgers.Where(expression).ToListAsync();
        }

        public async Task<JamaatLedger> Update(JamaatLedger jamaatLedger)
        {
            _context.JamaatLedgers.Update(jamaatLedger);
            return jamaatLedger;
        }
    }
}
