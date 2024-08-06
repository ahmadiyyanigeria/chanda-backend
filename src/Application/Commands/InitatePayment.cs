using Application.Contracts;
using Application.DTOs;
using Application.Exceptions;
using Application.Helpers;
using Application.Repositories;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class InitatePayment
    {
        public record Command : IRequest<InitiateResponse>
        {
            public string Reference { get; init; } = default!;            
        }

        public class Handler : IRequestHandler<Command, InitiateResponse>
        {
            private readonly IConfiguration _config;
            private readonly ICurrentUser _currentUser;
            private readonly IPaymentService _paymentService;
            private readonly IInvoiceRepository _invoiceRepository;

            public Handler(IConfiguration config, ICurrentUser currentUser, IPaymentService paymentService, IInvoiceRepository invoiceRepository)
            {
                _config = config;
                _currentUser = currentUser;
                _paymentService = paymentService;
                _invoiceRepository = invoiceRepository;
            }

            public async Task<InitiateResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var member = new MemberDetials { Name = "Ade Ola", ChandaNo = "0001", Email = "adeola@example.com", JamaatId = new Guid("5f9013da-5c0a-4af0-ae51-738f6bc0009d"), Role = "Jamaat-President" };//_currentUser.GetMemberDetails();
                if (member is null || string.IsNullOrEmpty(member.ChandaNo))
                {
                    throw new NotFoundException($"Please login to create an invoice.", ExceptionCodes.MemberNotFound.ToString(), 403);
                }

                var invoice = _invoiceRepository.Get(i => i.Reference == request.Reference);
                if (invoice is null)
                {
                    throw new NotFoundException("Invoice cannot be found.", ExceptionCodes.InvoiceNotFound.ToString(), 404);
                }
                var invoiceRef = request.Reference;
                var reference = Utility.GenerateReference("PAY");
                var paymentModel = new PaymentRequest
                {
                    reference = reference,
                    email = !string.IsNullOrEmpty(member.Email) ? member.Email : "paymentinfo@amjn.com",
                    amount = invoice.Amount * 100,
                    currency = "NGN",
                    callback_url = $"{_config["Payment:CallbackUrl"]!}Payments/Callback/{invoiceRef}"
                };

                var result = await _paymentService.InitiatePaymentAsync(paymentModel);
                return result;
            }
        }
    }
}
