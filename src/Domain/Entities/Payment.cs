using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment: BaseEntity
    {
        public Guid InvoiceId { get; private set; }
        public decimal Amount { get; private set; }
        public PaymentOption Option { get; private set; }
        public Invoice Invoice { get; private set; } = default!;

        public Payment(Guid invoiceId, decimal amount, PaymentOption option, string createdBy)
        {
            InvoiceId = invoiceId;
            Amount = amount;
            Option = option;
            CreatedBy = createdBy;
        }
    }
}
