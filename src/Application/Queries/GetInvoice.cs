﻿using Application.Exceptions;
using Application.Repositories;
using Domain.Enums;
using Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries
{
    public class GetInvoice
    {
        public record Query(Guid id) : IRequest<InvoiceResponse>;
        public record InvoiceResponse(Guid Id, string JamaatName, string Reference, decimal Amount, InvoiceStatus Status, IReadOnlyList<InvoiceItemResponse> InvoiceItems);
        public record InvoiceItemResponse(Guid Id, string MonthPayedFor, string PayerName, string ReceiptNo, int Year, decimal Amount, IReadOnlyList<ChandaItemResponse> ChandaItems);
        public record ChandaItemResponse(Guid Id, string ChandaTypeName, decimal Amount);

        public class Handler(IInvoiceRepository _invoiceRepository) : IRequestHandler<Query, InvoiceResponse>
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
