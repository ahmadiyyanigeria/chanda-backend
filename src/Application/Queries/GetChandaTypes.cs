using Application.Paging;
using Application.Repositories;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetChandaTypes
    {
        public record ChandaTypeResponse(Guid Id, string Name, string Code, string Description, Guid IncomeAccountId);

        public record Query(bool UsePaging) : PageRequest, IRequest<IReadOnlyList<ChandaTypeResponse>>;

        public class Handler : IRequestHandler<Query, IReadOnlyList<ChandaTypeResponse>>
        {
            private readonly IChandaTypeRepository _chandaTypeRepository;

            public Handler(IChandaTypeRepository chandaTypeRepository)
            {
                _chandaTypeRepository = chandaTypeRepository;
            }
            public async Task<IReadOnlyList<ChandaTypeResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var chandaTypes = await _chandaTypeRepository.GetAllAsync(request, ac=>ac.IsDeleted==false, request.UsePaging);

                return chandaTypes.Adapt<IReadOnlyList<ChandaTypeResponse>>();
            }

            
    }   }
}  
