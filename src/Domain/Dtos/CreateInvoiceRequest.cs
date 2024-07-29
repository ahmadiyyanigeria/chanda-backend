using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class CreateInvoiceRequest
    {
        public string JamaatCode { get; set; } = default!;
        public string InitiatorChandaNo { get; set; } = default!;
        public List<InvoiceItemRequest> InvoiceItems { get; set; } = new();
    }

    public class InvoiceItemRequest
    {
        public string PayerNo { get; set; } = default!;
        public MonthOfTheYear MonthPaidFor { get; set; }
        public int Year { get; set; }
        public List<ChandaItemRequest> ChandaItems { get; set; } = new();
    }

    public class ChandaItemRequest
    {
        public string ChandaTypeCode { get; set; } = default!;
        public decimal Amount { get; set; }
    }
}
