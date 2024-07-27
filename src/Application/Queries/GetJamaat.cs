using Application.Repositories;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetJamaat
    {
        public record JamaatResponse(string Name, Guid CircuitId, string CreatedBy, string? ModifiedBy, DateTime CreatedOn, DateTime? ModifiedOn);
        public record Query(Guid Id) : IRequest<JamaatResponse>;
       
        public class Handler : IRequestHandler<Query, JamaatResponse>
        {
            private readonly IJamaatRepository _jamaatRepository;
            public Handler(IJamaatRepository jamaatRepository)
            {
                _jamaatRepository = jamaatRepository;
            }
            public async Task<JamaatResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var jamaat = await _jamaatRepository.Get(j => j.Id == request.Id);
                return jamaat.Adapt<JamaatResponse>();
            }
             
        }
    }
}
