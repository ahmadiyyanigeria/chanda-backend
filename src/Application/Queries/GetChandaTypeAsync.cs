using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetChandaTypeAsync
    {
        
        public class Query : IRequest<ChandaTypeResponse>
        {
            public Guid Id { get; set; }

            public Query(Guid id)
            {
                Id = id;
            }
        }

        
        public class GetQueryHandler : IRequestHandler<Query, ChandaTypeResponse>
        {
            private readonly IChandaTypeRepository _chandaTypeRepository;

            public GetQueryHandler(IChandaTypeRepository chandaTypeRepository)
            {
                _chandaTypeRepository = chandaTypeRepository;
            }

            public  async Task<ChandaTypeResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var chanda =  await _chandaTypeRepository.GetByIdAsync(request.Id);
                return chanda.Adapt<ChandaTypeResponse>();
            }
        }

        public record ChandaTypeResponse(string Name,string Code,Guid IncomeAccountId, string Description, DateTime CreatedDate, DateTime? ModifiedDate);

    }
}
