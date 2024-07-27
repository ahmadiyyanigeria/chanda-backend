using Domain.Enums;

namespace Domain.Entities
{
    public class InvoiceItem: BaseEntity
    {
        public Guid PayerId { get; private set; }
        public Guid InvoiceId { get; private set; }
        public MonthOfTheYear MonthPaidFor {  get; private set; }
        public int Year {  get; private set; }
        public decimal Amount { get; private set; }
        public Member Member { get; private set; } = default!;
        public Invoice Invoice { get; private set; } = default!;
        public IReadOnlyList<ChandaItem> ChandaItems 
        { 
            get => _chandaItems.AsReadOnly(); 
            private set => _chandaItems.AddRange(value);
        }

        private readonly List<ChandaItem> _chandaItems = new();

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

        public InvoiceItem(Guid id, Guid payerId, Guid invoiceId, decimal amount, MonthOfTheYear monthPaidFor, int year, Member member, string createdBy)
        {
            Id = id;
            PayerId = payerId;
            InvoiceId = invoiceId;
            Amount = amount;
            MonthPaidFor = monthPaidFor;
            Year = year;
            Member = member;
            CreatedBy = createdBy;
        }
    }
}
