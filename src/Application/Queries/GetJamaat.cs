using Application.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Queries.GetJamaats;

namespace Application.Queries
{
    public class GetJamaat
    {
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
                if (jamaat == null)
                {
                    throw new DomainException(
                       message: "Jama'at not found",
                       errorCode: ExceptionCodes.JamaatNotFound.ToString(),
                       statusCode: 404
                   );
                }
                return jamaat.Adapt<JamaatResponse>();
            }
             
        }
    }
}
