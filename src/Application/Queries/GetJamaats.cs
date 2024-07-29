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
    public class GetJamaats
    {
        public record JamaatResponse(Guid Id, string Name, string Code, Guid CircuitId, string CircuitName, DateTime CreatedDate, DateTime? ModifiedDate);

        
        public record Query : PageRequest, IRequest<IReadOnlyList<JamaatResponse>>
        {
            public Guid? CircuitId { get; set; }
            public bool UsePaging { get; init; } = true;

            public Query(Guid? circuitId, bool usePaging)
            {
                CircuitId = circuitId;
                UsePaging = usePaging;
            }
        }

        
        public class GetJammatsByCircuitIdQueryHandler : IRequestHandler<Query, IReadOnlyList<JamaatResponse>>
        {
            private readonly IJamaatRepository _jamaatRepository;

            public GetJammatsByCircuitIdQueryHandler(IJamaatRepository jamaatRepository)
            {
                _jamaatRepository = jamaatRepository;
            }

            public async Task<IReadOnlyList<JamaatResponse>> Handle(Query request,  CancellationToken cancellationToken)
            {
                var jamaats = request.CircuitId == null 
                    ? await _jamaatRepository.GetAllAsync(request, j => !j.IsDeleted, request.UsePaging)
                : await _jamaatRepository.GetAllAsync(request, ja => ja.CircuitId == request.CircuitId, request.UsePaging );
                return jamaats.Adapt<IReadOnlyList<JamaatResponse>>();

            }
        }

    }
}
