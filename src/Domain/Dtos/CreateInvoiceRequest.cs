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
        public Guid JamaatId { get; set; }
        public string InitiatorChandaNo { get; set; } = default!;
        public List<InvoiceItemRequest> InvoiceItems { get; set; } = new();
    }

    public class InvoiceItemRequest
    {
        public Guid PayerId { get; set; }
        public MonthOfTheYear MonthPaidFor { get; set; }
        public int Year { get; set; }
        public List<ChandaItemRequest> ChandaItems { get; set; } = new();
    }

    public class ChandaItemRequest
    {
        public Guid ChandaTypeId { get; set; }
        public decimal Amount { get; set; }
    }
}
