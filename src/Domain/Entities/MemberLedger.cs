using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MemberLedger: BaseEntity
    {
        public Guid MemberId { get; private set; }
        public Member Member { get; private set; } = default!;
        public IReadOnlyList<Ledger> LedgerList 
        { 
            get => _ledgers.AsReadOnly(); 
            private set => _ledgers.AddRange(value);
        }

        private readonly List<Ledger> _ledgers = [];

        public MemberLedger(Guid memberId, string createdBy)
        {
            MemberId = memberId;
            CreatedBy = createdBy;
        }

        public void AddLedgers(params Ledger[] ledgers)
        {
            foreach (var ledger in ledgers)
            {
                if(!_ledgers.Any(l => l.Id == ledger.Id))
                {
                    _ledgers.Add(ledger);
                }
            }
        }
    }
}
