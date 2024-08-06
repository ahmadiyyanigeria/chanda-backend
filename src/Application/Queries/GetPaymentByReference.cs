using Application.Exceptions;
using Application.Repositories;
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
    public class GetPaymentByReference
    {
        public record Query(string Reference) : IRequest<PaymentResponse>;
            public record PaymentResponse(string InvoiceReference, string PaymentReference, decimal Amount, string Option);
        public class Handler(IPaymentRepository _paymentRepository) : IRequestHandler<Query, PaymentResponse>
        {
            public async Task<PaymentResponse> Handle(Query request, CancellationToken cancellationToken)
            {
                var payment = await _paymentRepository.GetAsync(p => p.Reference.Equals(request.Reference));
                if(payment is null)
                {
                    throw new NotFoundException("Payment not found", ExceptionCodes.PaymentNotFound.ToString(), 404);
                }
                return payment.Adapt<PaymentResponse>();
            }
        }
    }
}
