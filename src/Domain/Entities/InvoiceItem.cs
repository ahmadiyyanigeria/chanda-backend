namespace Domain.Entities
{
    public class InvoiceItem: BaseEntity
    {
        public Guid PayerId { get; private set; }
        public Guid InvoiceId { get; private set; }
        public string MonthPaidFor {  get; private set; }
        public Member Member { get; private set; } = default!;
        public Invoice Invoice { get; private set; } = default!;
        public IReadOnlyList<ChandaItem> ChandaItems 
        { 
            get => _chandaItems.AsReadOnly(); 
            private set => _chandaItems.AddRange(value);
        }

        private readonly List<ChandaItem> _chandaItems = new();

        public void AddChandaItems(params ChandaItem[] chandaItems)
        {
            foreach (var chandaItem in chandaItems)
            {
                if(!_chandaItems.Any(item => item.Id == chandaItem.Id))
                {
                    _chandaItems.Add(chandaItem);
                }
            }
        }

        public InvoiceItem(Guid payerId, Guid invoiceId, string monthPaidFor, string createdBy)
        {
            PayerId = payerId;
            InvoiceId = invoiceId;
            MonthPaidFor = monthPaidFor;
            CreatedBy = createdBy;
        }
    }
}
