using Application.Contracts;
using Application.DTOs;

namespace Infrastructure.Services.PaymentServices
{
    public class VirtualPaymentService : IPaymentService
    {
        public Task<InitiateResponse> InitiatePaymentAsync(PaymentRequest model)
        {
            throw new NotImplementedException();
        }

        public Task<VerificationResponse> VerifyPaymentAsync(string reference)
        {
            throw new NotImplementedException();
        }
    }
}
