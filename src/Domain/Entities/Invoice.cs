using Domain.Enums;

namespace Domain.Entities
{
    public class Invoice: BaseEntity
    {
        public Guid JamaatId { get; private set; }
        public decimal Amount { get; private set; }
        public InvoiceStatus Status { get; private set; }
        public Jamaat Jamaat { get; private set; } = default!;
        public IReadOnlyList<InvoiceItem> InvoiceItems 
        { 
            get => _invoiceItem.AsReadOnly(); 
            private set => _invoiceItem.AddRange(value);
        }

        public IReadOnlyList<Payment> Payments
        {
            get => _payments.AsReadOnly();
            private set => _payments.AddRange(value);
        }

        private readonly List<InvoiceItem> _invoiceItem = new();
        private readonly List<Payment> _payments = new();

        public Invoice(Guid jamaatId, decimal amount, InvoiceStatus status, string createdBy)
        {
            JamaatId = jamaatId;
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

        public void AddInvoiceItems(params InvoiceItem[] invoiceItems)
        {
            foreach(var invoiceItem in invoiceItems)
            {
                if(!_invoiceItem.Any(item => item.Id == invoiceItem.Id))
                {
                    _invoiceItem.Add(invoiceItem);
                }
            }
        }
    }
}
