using Application.Repositories;
using Domain.Exceptions;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetCircuit
    {
        public record CircuitResponse(string Name, string CreatedBy, string? ModifiedBy, DateTime CreatedOn, DateTime? ModifiedOn);

        public record Query(Guid Id) : IRequest<CircuitResponse>;

        public class Handler : IRequestHandler<Query, CircuitResponse>
        {
            private readonly ICircuitRepository _circuitRepository;
            
            public Handler(ICircuitRepository circuitRepository) 
            {
                _circuitRepository = circuitRepository;
            }

            public async Task<CircuitResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var circuit = await _circuitRepository.GetAsync(c => c.Id == request.Id);
                if(circuit == null)
                {
                    throw new DomainException(
                       message: "Circuit not found",
                       errorCode: ExceptionCodes.CircuitNotFound.ToString(),
                       statusCode: 404
                   );
                }
                return circuit.Adapt<CircuitResponse>();
            }
        }
    }
    
}
