using Application.Contracts;
using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Mapster;
using MediatR;
using static Application.Queries.GetPaymentByReference;

namespace Application.Queries
{
    public class PaymentCallback
    {
        public record Query(string Reference, string InvoiceRef): IRequest<CallbackResponse>;

        public record CallbackResponse
        {
            public bool Status { get; init; }
            public string Message { get; init; } = string.Empty;
            public PaymentResponse? PaymentResponse { get; init; }
        }

        public class Handler : IRequestHandler<Query, CallbackResponse>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPaymentService _paymentService;
            private readonly ILedgerRepository _ledgerRepository;
            private readonly IPaymentRepository _paymentRepository;
            private readonly IInvoiceRepository _invoiceRepository;

            public Handler(IUnitOfWork unitOfWork, IPaymentService paymentService, ILedgerRepository ledgerRepository, IPaymentRepository paymentRepository, IInvoiceRepository invoiceRepository)
            {
                _unitOfWork = unitOfWork;
                _paymentService = paymentService;
                _ledgerRepository = ledgerRepository;
                _paymentRepository = paymentRepository;
                _invoiceRepository = invoiceRepository;
            }

            public async Task<CallbackResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = await _paymentService.VerifyPaymentAsync(request.Reference);
                if (response is not null && response.Status) 
                {
                    var invoice = await _invoiceRepository.GetAsync(i => i.Reference == request.InvoiceRef);
                    if (invoice is null) 
                    {
                        throw new NotFoundException("Invoice not found.", ExceptionCodes.InvoiceNotFound.ToString(), 400);
                    }
                    var payment = new Payment(invoice.Id, request.Reference, response.Data!.Amount/100, Domain.Enums.PaymentOption.Card, invoice.CreatedBy);

                    var createdBy = invoice.CreatedBy;
                    var legders = new List<Ledger>();
                    foreach(var item in invoice.InvoiceItems)
                    {
                        var monthPaidFor = item.MonthPaidFor;
                        var year = item.Year;
                        var memberLedgerId = item.Member.MemberLedgerId;
                        var jamaatLedgerId = item.Member.Jamaat.JamaatLedgerId;
                        foreach (var chanda in item.ChandaItems)
                        {
                            var ledger = new Ledger(chanda.ChandaTypeId, monthPaidFor, year, chanda.Amount, payment.CreatedOn, "Payment Successful", memberLedgerId, jamaatLedgerId, createdBy);

                            item.Member.MemberLedger.AddLedgers(ledger);
                            item.Member.Jamaat.JamaatLedger.AddLedgers(ledger);
                            legders.Add(ledger);
                        }
                    }

                    payment = await _paymentRepository.AddAsync(payment);
                    legders = await _ledgerRepository.AddRangeAsync(legders);
                    await _invoiceRepository.UpdateAsync(invoice);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    //TODO:
                    //Publish ledgers to General Ledger Account....

                    return new CallbackResponse
                    {
                        Status = true,
                        Message = response.Message,
                        PaymentResponse = payment.Adapt<PaymentResponse>()
                    };
                }

                return new CallbackResponse
                {
                    Message = response!.Message,
                };
            }
        }
    }
}
