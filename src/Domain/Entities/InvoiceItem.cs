using Domain.Enums;

namespace Domain.Entities
{
    public class InvoiceItem: BaseEntity
    {
        public Guid PayerId { get; private set; }
        public Guid JamaatId { get; private set; }
        public Guid InvoiceId { get; private set; }
        public string? ReceiptNo { get; private set; }
        public string ReferenceNo { get; private set; }
        public MonthOfTheYear MonthPaidFor {  get; private set; }
        public int Year {  get; private set; }
        public decimal Amount { get; private set; }
        public Member Member { get; private set; } = default!;
        public Jamaat Jamaat { get; private set; } = default!;
        public Invoice Invoice { get; private set; } = default!;
        public IList<ChandaItem> ChandaItems 
        { 
            get => _chandaItems.ToList(); 
            private set => _chandaItems.AddRange(value);
        }

        private readonly List<ChandaItem> _chandaItems = [];

        public void AddChandaItems(List<ChandaItem> chandaItems)
        {
            foreach (var chandaItem in chandaItems)
            {
                if(!_chandaItems.Any(item => item.Id == chandaItem.Id))
                {
                    _chandaItems.Add(chandaItem);
                }
            }
        }

        public InvoiceItem(Guid id, Guid payerId, Guid jamaatId, Guid invoiceId, string? receiptNo, string referenceNo, decimal amount, MonthOfTheYear monthPaidFor, int year, string createdBy)
        {
            Id = id;
            PayerId = payerId;
            JamaatId = jamaatId;
            InvoiceId = invoiceId;
            ReceiptNo = receiptNo;
            ReferenceNo = referenceNo;
            Amount = amount;
            MonthPaidFor = monthPaidFor;
            Year = year;
            CreatedBy = createdBy;
        }
    }
}
