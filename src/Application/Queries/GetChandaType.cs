using Application.Exceptions;
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

namespace Application.Queries
{
    public class GetChandaType
    {
        
        public record Query(Guid Id) : IRequest<ChandaTypeResponse>;
        
        
        public class Handler : IRequestHandler<Query, ChandaTypeResponse>
        {
            private readonly IChandaTypeRepository _chandaTypeRepository;

            public Handler(IChandaTypeRepository chandaTypeRepository)
            {
                _chandaTypeRepository = chandaTypeRepository;
            }

            public  async Task<ChandaTypeResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var chandaType =  await _chandaTypeRepository.GetByIdAsync(request.Id);
                if(chandaType is null)
                {
                    throw new NotFoundException("Chanda Type Not Found", ExceptionCodes.ChandaTypeNotFound.ToString(), 404);
                }
                return chandaType.Adapt<ChandaTypeResponse>();
            }
        }

        public record ChandaTypeResponse(Guid Id, string Name,string Code,Guid IncomeAccountId, string Description, DateTime CreatedOn, DateTime? ModifiedOn);

    }
}
