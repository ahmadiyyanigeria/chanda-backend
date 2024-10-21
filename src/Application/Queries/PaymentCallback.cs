using Application.Contracts;
using Application.Exceptions;
using Application.Mailing;
using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Mapster;
using MediatR;
using static Application.Queries.GetPaymentByReference;

namespace Application.Queries
{
    public class PaymentCallback
    {
        public record Query(string Reference): IRequest<CallbackResponse>;

        public record CallbackResponse
        {
            public bool Status { get; init; }
            public string Message { get; init; } = string.Empty;
            public PaymentResponse? PaymentResponse { get; init; }
        }

        public class Handler : IRequestHandler<Query, CallbackResponse>
        {
            private readonly IMailProvider _email;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILedgerRepository _ledgerRepository;
            private readonly IPaymentRepository _paymentRepository;
            private readonly IInvoiceRepository _invoiceRepository;
            private readonly IPaymentGatewayFactory _paymentServiceFactory;

            public Handler(IUnitOfWork unitOfWork, ILedgerRepository ledgerRepository, IPaymentRepository paymentRepository, IInvoiceRepository invoiceRepository, IPaymentGatewayFactory paymentServiceFactory, IMailProvider email)
            {
                _email = email;
                _unitOfWork = unitOfWork;
                _ledgerRepository = ledgerRepository;
                _paymentRepository = paymentRepository;
                _invoiceRepository = invoiceRepository;
                _paymentServiceFactory = paymentServiceFactory;
            }

            public async Task<CallbackResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var payment = await _paymentRepository.GetAsync(p => p.Reference == request.Reference);
                if (payment is null)
                {
                    throw new NotFoundException("Payment not found.", ExceptionCodes.PaymentNotFound.ToString(), 404);
                }

                var _paymentService = _paymentServiceFactory.GetPaymentService(payment.Option);
                var response = await _paymentService.VerifyPaymentAsync(request.Reference);
                if (response is not null && response.Status) 
                {                    
                    var invoice = await _invoiceRepository.GetAsync(i => i.Id == payment.InvoiceId);
                    if (invoice is null) 
                    {
                        throw new NotFoundException("Invoice not found.", ExceptionCodes.InvoiceNotFound.ToString(), 404);
                    }
                    
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

                    invoice.UpdateStatus(InvoiceStatus.Paid);
                    payment.UpdateStatus(PaymentStatus.Confirmed);
                    payment = _paymentRepository.Update(payment);
                    legders = await _ledgerRepository.AddRangeAsync(legders);
                    await _invoiceRepository.UpdateAsync(invoice);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    //Send mail for each payment...
                    await SendPaymentConfirmationEmail(invoice.InvoiceItems);

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

            private async Task SendPaymentConfirmationEmail(IReadOnlyList<InvoiceItem> invoiceItems)
            {
                foreach (var item in invoiceItems)
                {
                    var purpose = string.Join(", ", item.ChandaItems.Select(i => i.ChandaType.Name).ToList());
                    var body = $"Asalam alaykum waramotulah wabarakatuhu," +
                        $"Dear {item.Member.Name}," +
                        $"\n\nThis is to notify you of successful payment of the sum of {item.Amount} for {purpose} in the month of {item.MonthPaidFor} in the year {item.Year}." +
                        $"" +
                        $"\nJazakumllah Khairan.";

                    var mailRequest = new MailRequest(item.Member.Email, $"Payment Confirmation", body, "info.amjn@amjn.com");

                    await _email.SendAsync(mailRequest, CancellationToken.None);                    
                }
            }
        }
    }
}
