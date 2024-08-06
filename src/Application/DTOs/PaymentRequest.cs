using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record PaymentRequest
    {
        public string email { get; set; } = default!;
        public decimal amount { get; set; }        
        public string reference { get; set; } = default!;
        public string currency { get; set; } = default!;
        public string callback_url { get; set; } = default!;
    }

    public record InitiateResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; } = default!;
        public UrlDetails? Data { get; set; }
    }

    public record UrlDetails
    {
        public string Authorization_url { get; set; } = default!;
        public string Access_code { get; set; } = default!;
        public string Reference { get; set; } = default!;
    }
}
