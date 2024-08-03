using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs
{
    public record GroupInvoiceRequest
    {
        public InvoiceType Type { get; init; }
        public IReadOnlyList<GroupInvoiceItemRequest> InvoiceItems { get; init; } = new List<GroupInvoiceItemRequest>();
    }
    public record GroupInvoiceItemRequest
    {
        public string ChandaNo { get; init; } = default!;
        public string ReceiptNo { get; init; } = default!;
        public MonthOfTheYear MonthPaidFor { get; init; }
        public int Year { get; init; }
        public IReadOnlyList<ChandaItemCommand> ChandaItems { get; init; } = new List<ChandaItemCommand>();
    }
    public record ChandaItemCommand(string ChandaTypeName, decimal Amount);

    public record InvoiceFromFileRequest
    {
        public InvoiceType Type { get; init;}
        public required IFormFile formFile { get; init; }
    }
}
