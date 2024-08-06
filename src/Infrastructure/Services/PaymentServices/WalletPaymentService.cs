using Application.Contracts;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.PaymentServices
{
    public class WalletPaymentService : IPaymentService
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
