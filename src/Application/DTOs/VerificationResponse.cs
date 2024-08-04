using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record VerificationResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; } = default!;
        public Data? Data { get; set; }
    }

    public record Data
    {
        public long Id { get; set; }
        public string? Domain { get; set; }
        public string? Status { get; set; }
        public string? Reference { get; set; }
        public decimal Amount { get; set; }
        public string? Message { get; set; }
        public string? Gateway_Response { get; set; }
        public string? Paid_At { get; set; }
        public string? Created_At { get; set; }
        public string? Channel { get; set; }
        public string? Currency { get; set; }
        public string? Ip_Address { get; set; }
    }
}
