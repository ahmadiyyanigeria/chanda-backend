using Application.Paging;
using Application.Repositories;
using Domain.Constants;
using Domain.Entities;
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
    public class GetAllJammatInACircuitAsync
    {
        public record JammatResponse(string Name, Guid CircuitId, DateTime CreatedDate, DateTime? ModifiedDate);

        
        public record GetJammatsByCircuitIdQuery : PageRequest, IRequest<List<JammatResponse>>
        {
            public Guid CircuitId { get; set; }
            public bool UsePaging { get; init; } = true;

            public GetJammatsByCircuitIdQuery(Guid circuitId, bool usePaging)
            {
                CircuitId = circuitId;
                UsePaging = usePaging;
            }
        }

        
        public class GetJammatsByCircuitIdQueryHandler : IRequestHandler<GetJammatsByCircuitIdQuery, List<JammatResponse>>
        {
            private readonly IJamaatRepository _jamaatRepository;

            public GetJammatsByCircuitIdQueryHandler(IJamaatRepository jamaatRepository)
            {
                _jamaatRepository = jamaatRepository;
            }

            public async Task<List<JammatResponse>> Handle(GetJammatsByCircuitIdQuery request,  CancellationToken cancellationToken)
            {
                
                var jam = await _jamaatRepository.GetAll(request, ja => ja.CircuitId == request.CircuitId, request.UsePaging  );
                return jam.Adapt<List<JammatResponse>>();

            }
        }

    }
}
