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
    public class JamaatRepository(AppDbContext _context) : IJamaatRepository
    {
        public async Task<Jamaat> Add(Jamaat jamaat)
        {
            await _context.Jamaats.AddAsync(jamaat);
            return jamaat;
        }

        public async Task Delete(Jamaat jamaat)
        {
            _context.Jamaats.Remove(jamaat);
        }

        public async Task<Jamaat> Get(Expression<Func<Jamaat, bool>> expression)
        {
            return await _context.Jamaats.SingleOrDefaultAsync(expression);
        }

        public async Task<IList<Jamaat>> GetAll(Expression<Func<Jamaat, bool>> expression)
        {
            return await _context.Jamaats.Where(expression).ToListAsync();
        }

        public async Task<Jamaat> Update(Jamaat jamaat)
        {
            _context.Jamaats.Update(jamaat);
            return jamaat;
        }
    }
}
