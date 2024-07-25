using Application.Paging;
using Application.Repositories;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Queries.GetRoles;

namespace Application.Queries
{
    public class GetAllChandaTypesAsync
    {
        public record ChandaTypeResponse(string Name, string Code, string Description, Guid IncomeAccountId);

        public record Query : PageRequest, IRequest<IList<ChandaTypeResponse>>
        {
            public bool UsePaging { get; init; } = true;
        }

        public class Handler : IRequestHandler<Query, IList<ChandaTypeResponse>>
        {
            private readonly IChandaTypeRepository _chandaTypeRepository;

            public Handler(IChandaTypeRepository chandaTypeRepository)
            {
                _chandaTypeRepository = chandaTypeRepository;
            }
            public async Task<IList<ChandaTypeResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var chandaTypes = await _chandaTypeRepository.GetAllAsync(request, ac=>ac.IsDeleted==false, request.UsePaging);

                return chandaTypes.Adapt<IList<ChandaTypeResponse>>();
            }

            
    }   }
}  
