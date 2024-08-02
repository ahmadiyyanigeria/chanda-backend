using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record GroupInvoiceRequest
    {
        public InvoiceType Type { get; init; }
        public IReadOnlyList<InvoiceItemCommand> InvoiceItems { get; init; } = new List<InvoiceItemCommand>();
    }
    public record InvoiceItemCommand
    {
        public string PayerNo { get; init; } = default!;
        public string ReceiptNo { get; init; } = default!;
        public MonthOfTheYear MonthPaidFor { get; init; }
        public int Year { get; init; }
        public IReadOnlyList<ChandaItemCommand> ChandaItems { get; init; } = new List<ChandaItemCommand>();
    }
    public record ChandaItemCommand(string ChandaTypeCode, decimal Amount);
}
