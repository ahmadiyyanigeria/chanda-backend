namespace Domain.Entities
{
    public class ChandaItem: BaseEntity
    {
        public Guid InvoiceItemId { get; private set; }
        public Guid ChandaTypeId { get; private set; }
        public decimal Amount { get; private set; }
        public InvoiceItem InvoiceItem { get; private set; } = default!;
        public ChandaType ChandaType { get; private set;} = default!;

        public ChandaItem(Guid invoiceItemId, Guid chandaTypeId, decimal amount, ChandaType chandaType, string createdBy)
        {
            InvoiceItemId = invoiceItemId;
            ChandaTypeId = chandaTypeId;
            Amount = amount;
            ChandaType = chandaType;
            CreatedBy = createdBy;
        }
    }
}
