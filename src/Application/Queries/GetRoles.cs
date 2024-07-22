using Application.Paging;
using Application.Repositories;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetRoles
    {
        public record Query : PageRequest, IRequest<PaginatedList<RoleResponse>>
        {
            public bool UsePaging { get; init; } = true;
        }

        public class Handler : IRequestHandler<Query, PaginatedList<RoleResponse>>
        {
            private readonly IRoleRepository _roleRepository;

            public Handler(IRoleRepository roleRepository)
            {
                _roleRepository = roleRepository;
            }
            public async Task<PaginatedList<RoleResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var roles = await _roleRepository.GetRoles(request, request.UsePaging);

                return roles.Adapt<PaginatedList<RoleResponse>>();
            }
        }

        public record RoleResponse(string Name, string Description, DateTime CreatedDate, DateTime? ModifiedDate);
    }
}
