using Domain.Enums;

namespace Domain.Entities
{
    public class Invoice: BaseEntity
    {
        public Guid JamaatId { get; private set; }
        public string Reference { get; private set; }
        public decimal Amount { get; private set; }
        public InvoiceStatus Status { get; private set; }
        public Jamaat Jamaat { get; private set; } = default!;
        public IReadOnlyList<InvoiceItem> InvoiceItems
        {
            get => _invoiceItems.AsReadOnly();
            private set => _invoiceItems.AddRange(value);
        }
        
        public IReadOnlyList<Payment> Payments 
        {
            get => _payments.AsReadOnly();
            private set => _payments.AddRange(value);
        }

        private readonly List<InvoiceItem> _invoiceItems = [];
        private readonly List<Payment> _payments = [];

        public Invoice(Guid id, Guid jamaatId, string reference, decimal amount, InvoiceStatus status, string createdBy)
        {
            Id = id;
            JamaatId = jamaatId;
            Reference = reference;
            Amount = amount;
            Status = status;
            CreatedBy = createdBy;
        }

        public void AddPayments(params Payment[] payments)
        {
            foreach (var payment in payments)
            {
                if(!_payments.Any(p => p.Id == payment.Id))
                {
                    _payments.Add(payment);
                }
            }
        }

        public void AddInvoiceItems(List<InvoiceItem> invoiceItems)
        {
            foreach(var invoiceItem in invoiceItems)
            {
                if(!_invoiceItems.Any(item => item.Id == invoiceItem.Id))
                {
                    _invoiceItems.Add(invoiceItem);
                }
            }
        }

        public void UpdateAmount(decimal amount)
        {
            Amount = amount;
        }
    }
}
