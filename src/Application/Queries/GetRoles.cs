using Application.Paging;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetRoles
    {
        public record Query : PageRequest, IRequest<List<RoleResponse>>
        {
            public bool UsePaging { get; init; } = true;
        }

        public class Handler : IRequestHandler<Query, List<RoleResponse>>
        {
            private readonly IRoleRepository _roleRepository;

            public Handler(IRoleRepository roleRepository)
            {
                _roleRepository = roleRepository;
            }
            public async Task<List<RoleResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var roles = await _roleRepository.GetRoles(request, request.UsePaging);
                return roles.Select(r => new RoleResponse
                (
                    r.Name,
                    r.Description,
                    r.CreatedOn,
                    r.ModifiedOn
                )).ToList();
            }
        }

        public record RoleResponse(string Name, string Description, DateTime CreatedDate, DateTime? ModifiedDate);
    }
}
