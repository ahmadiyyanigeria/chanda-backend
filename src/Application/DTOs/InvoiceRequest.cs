using Domain.Enums;

namespace Application.DTOs
{
    public record InvoiceRequest
    {
        public List<InvoiceItemRequest> InvoiceItems { get; set; } = new();
    }

    public record InvoiceItemRequest
    {
        public string ReceiptNo { get; init; } = default!;
        public MonthOfTheYear MonthPaidFor { get; set; }
        public int Year { get; set; }
        public List<ChandaItemRequest> ChandaItems { get; set; } = new();
    }

    public record ChandaItemRequest
    {
        public string ChandaTypeName { get; set; } = default!;
        public decimal Amount { get; set; }
    }
}
