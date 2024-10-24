using Application.Repositories;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CalculateChandaBaseOnIncome
    {
        public class ChandaResponse
        {
            public ChandaTypeName ChandaType { get; set; }
            public decimal ChandaAmount { get; set; }
            public decimal Income { get; set; }

        }

        public record Command : IRequest<ChandaResponse> 
        {
            public decimal Income { get; set; }
            public ChandaTypeName ChandaType { get; set; }
        } 

        public class Handler : IRequestHandler<Command, ChandaResponse> 
        {
            
            public async Task<ChandaResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                
                decimal chandaAmount = 0;

                switch (request.ChandaType)
                {
                    case ChandaTypeName.ChandaWassiyat:
                        chandaAmount = request.Income * 0.10m;
                        break;

                    case ChandaTypeName.ChandaAam:
                        chandaAmount = request.Income * 0.06m;
                        break;

                    case ChandaTypeName.ChandaJalsa:
                        chandaAmount = request.Income * 0.10m;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return new ChandaResponse
                {
                    ChandaType = request.ChandaType,
                    ChandaAmount = chandaAmount,
                    Income = request.Income
                };
            }
        }
    }
}
