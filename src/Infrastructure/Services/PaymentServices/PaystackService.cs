using Application.Contracts;
using Application.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Infrastructure.Services.PaymentServices
{
    public class PaystackService : IPaymentService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public PaystackService(IConfiguration config)
        {
            _config = config;
            _client = new HttpClient { BaseAddress = new Uri(_config["Payment:PaystackBaseUrl"]!) };
            _client.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue("Bearer", _config["Payment:PaystackKey"]);
        }

        public async Task<InitiateResponse?> InitiatePayment(PaymentRequest model)
        {
            var result = new InitiateResponse();
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/transaction/initialize", content);

            if (!response.IsSuccessStatusCode)
            {
                result.Message = response.ReasonPhrase ?? "Faied to initate payment.";
                return result;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<InitiateResponse>(responseContent);
            return result;
        }

        public async Task<VerificationResponse?> VerifyPayment(string reference)
        {
            var result = new VerificationResponse();
            var response = await _client.GetAsync($"/transaction/verify/{reference}");

            if (!response.IsSuccessStatusCode)
            {
                result.Message = response.ReasonPhrase ?? "Verification failed.";
                return result;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<VerificationResponse>(responseContent);

            return result;
        }
    }
}
