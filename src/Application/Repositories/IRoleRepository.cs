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
        Task<List<Role>> GetRoles(PageRequest pageRequest, bool usePaging = true);
    }
}
