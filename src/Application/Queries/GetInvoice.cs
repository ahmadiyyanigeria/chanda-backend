using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Mapster;
using MediatR;

namespace Application.Queries
{
    public class GetInvoice
    {
        public record Query(Guid id) : IRequest<InvoiceResponse>;
            public record InvoiceResponse(Guid JamaatName, string Reference, decimal Amount, InvoiceStatus Status, IReadOnlyList<InvoiceItemResponse> InvoiceItems);
        public record InvoiceItemResponse(string MonthPayedFor, string PayerName, int Year, decimal Amount, IReadOnlyList<ChandaItemResponse> ChandaItems);
        public record ChandaItemResponse(string ChandaType, decimal Amount);

        public class Handle(IInvoiceRepository _invoiceRepository) : IRequestHandler<Query, InvoiceResponse>
        {
            public async Task<InvoiceResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var invoice = await _invoiceRepository.GetAsync(i => i.Id == request.id);
                if (invoice == null)
                {
                    throw new NotFoundException("Invoice not found", ExceptionCodes.InvoiceNotFound.ToString(), 404);
                }
                return invoice.Adapt<InvoiceResponse>();
            }
        }
    }
}
