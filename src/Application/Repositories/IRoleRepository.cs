using Application.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IRoleRepository
    {
        Task<PaginatedList<Role>> GetRoles(PageRequest pageRequest, bool usePaging = true);
        Task<IReadOnlyList<Role>> GetRoles(IReadOnlyList<string> roleNames);
        Task<Role?> GetRole(string roleName);
        Task<bool> ExistsAsync(string name);
    }
}
