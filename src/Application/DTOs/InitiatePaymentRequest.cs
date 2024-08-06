﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record InitiatePaymentRequest
    {
        public string Reference { get; init; } = default!;
        public PaymentOption Option { get; set; }
    }
}
