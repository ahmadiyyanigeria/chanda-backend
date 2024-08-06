using Application.DTOs;

namespace Application.Contracts
{
    public interface IPaymentService
    {
        Task<InitiateResponse> InitiatePaymentAsync(PaymentRequest model);
        Task<VerificationResponse> VerifyPaymentAsync(string reference);
    }
}
