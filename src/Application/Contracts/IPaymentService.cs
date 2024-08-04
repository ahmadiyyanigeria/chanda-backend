using Application.DTOs;

namespace Application.Contracts
{
    public interface IPaymentService
    {
        Task<InitiateResponse?> InitiatePayment(PaymentRequest model);
        Task<VerificationResponse?> VerifyPayment(string reference);
    }
}
