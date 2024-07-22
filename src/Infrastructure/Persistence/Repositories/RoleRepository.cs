using Application.Paging;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Roles.AnyAsync(r => r.Name == name);
        }

        public async Task<Role?> GetRole(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<PaginatedList<Role>> GetRoles(PageRequest pageRequest, bool usePaging = true)
        {
            var query = _context.Roles.OrderBy(r => r.Name);

            if (!string.IsNullOrEmpty(pageRequest.Keyword))
            {
                query = query.Where(r => r.Name.Contains(pageRequest.Keyword, StringComparison.OrdinalIgnoreCase)).Order();
            }

            if (pageRequest.IsDescending)
            {
                query = query.OrderDescending();
            }

            var totalItemsCount = await query.CountAsync();
            if (usePaging)
            {
                var offset = (pageRequest.Page - 1) * pageRequest.PageSize;
                var result = await query.Skip(offset).Take(pageRequest.PageSize).ToListAsync();
                return result.ToPaginatedList(totalItemsCount, pageRequest.Page, pageRequest.PageSize);
            }
            else
            {
                var result = await query.ToListAsync();
                return result.ToPaginatedList(totalItemsCount, 1, totalItemsCount);
            }
        }

        public async Task<IReadOnlyList<Role>> GetRoles(IReadOnlyList<string> roleNames)
        {
            return await _context.Roles
                .Where(r => roleNames.Contains(r.Name))
                .ToListAsync();
        }
    }
}
