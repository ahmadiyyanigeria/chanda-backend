using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class JamaatLedger: BaseEntity
    {
        public Guid JamaatId { get; private set; }
        public Jamaat Jamaat { get; private set; } = default!;
        public IReadOnlyList<Ledger> LedgerList 
        { 
            get => _ledgers.AsReadOnly(); 
            private set => _ledgers.AddRange(value);
        }

        private readonly List<Ledger> _ledgers = [];

        public JamaatLedger(Guid jamaatId, string createdBy)
        {
            JamaatId = jamaatId;
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
