namespace Domain.Entities
{
    public class Ledger: BaseEntity
    {
        public Guid ChandaTypeId { get; private set; }
        public string MonthPaidFor { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime EntryDate { get; private set; }
        public string Description { get; private set; }
        public Guid? MemberLedgerId { get; private set; }
        public Guid? JamaatLedgerId { get; private set; }
        public ChandaType ChandaType { get; private set; } = default!;
        public MemberLedger? MemberLedger { get; private set; } = default!;
        public JamaatLedger? JamaatLedger { get; private set; } = default!;

        public Ledger(Guid chandaTypeId, string monthPaidFor, decimal amount, DateTime entryDate, string description, Guid? memberLedgerId, Guid? jamaatLedgerId, string createdBy)
        {
            ChandaTypeId = chandaTypeId;
            MonthPaidFor = monthPaidFor;
            Amount = amount;
            EntryDate = entryDate;
            Description = description;
            MemberLedgerId = memberLedgerId;
            JamaatLedgerId = jamaatLedgerId;
            CreatedBy = createdBy;
        }
    }
}
