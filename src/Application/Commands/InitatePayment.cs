using Application.Contracts;
using Application.DTOs;
using Application.Exceptions;
using Application.Helpers;
using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
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
            public PaymentOption Option { get; init; }
        }

        public class Handler : IRequestHandler<Command, InitiateResponse>
        {
            private readonly IConfiguration _config;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUser _currentUser;            
            private readonly IPaymentRepository _paymentRepository;
            private readonly IInvoiceRepository _invoiceRepository;
            private readonly IPaymentGatewayFactory _paymentServiceFactory;

            public Handler(IConfiguration config, IUnitOfWork unitOfWork, ICurrentUser currentUser, IPaymentRepository paymentRepository, IInvoiceRepository invoiceRepository, IPaymentGatewayFactory paymentServiceFactory)
            {
                _config = config;
                _unitOfWork = unitOfWork;
                _currentUser = currentUser;
                _paymentRepository = paymentRepository;
                _invoiceRepository = invoiceRepository;
                _paymentServiceFactory = paymentServiceFactory;
            }

            public async Task<InitiateResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var _paymentService = _paymentServiceFactory.GetPaymentService(request.Option);
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
                
                var reference = Utility.GenerateReference("PAY");
                var payment = new Payment(invoice.Id, reference, invoice.Amount, PaymentOption.Paystack, PaymentStatus.PendingVerification, invoice.CreatedBy);
                var paymentModel = new PaymentRequest
                {
                    reference = reference,
                    email = !string.IsNullOrEmpty(member.Email) ? member.Email : "paymentinfo@amjn.com",
                    amount = invoice.Amount * 100,
                    currency = "NGN",
                    callback_url = $"{_config["Payment:CallbackUrl"]!}Payments/Callback"
                };

                payment = await _paymentRepository.AddAsync(payment);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var result = await _paymentService.InitiatePaymentAsync(paymentModel);
                return result;
            }
        }
    }
}
