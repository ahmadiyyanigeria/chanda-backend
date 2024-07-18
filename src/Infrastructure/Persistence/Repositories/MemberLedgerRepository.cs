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
    public class MemberLedgerRepository(AppDbContext _context) : IMemberLedgerRepository
    {
        public async Task<MemberLedger> Add(MemberLedger memberLedger)
        {
            await _context.MemberLedgers.AddAsync(memberLedger);
            return memberLedger;
        }

        public async Task Delete(MemberLedger memberLedger)
        {
            _context.MemberLedgers.Remove(memberLedger);
        }

        public async Task<MemberLedger> Get(Expression<Func<MemberLedger, bool>> expression)
        {
            return await _context.MemberLedgers.SingleOrDefaultAsync(expression);
        }

        public async Task<IList<MemberLedger>> GetAll(Expression<Func<MemberLedger, bool>> expression)
        {
            return await _context.MemberLedgers.Where(expression).ToListAsync();
        }

        public async Task<MemberLedger> Update(MemberLedger memberLedger)
        {
            _context.MemberLedgers.Update(memberLedger);
            return memberLedger;
        }
    }
}
