using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Jamaat: BaseEntity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public Guid CircuitId { get; private set; }
        public Guid JamaatLedgerId { get; private set; }
        public Circuit Circuit { get; private set; } = default!;
        public JamaatLedger JamaatLedger { get; private set; } = default!;
        public IReadOnlyList<Member> Members 
        {   
            get => _members.AsReadOnly();
            private set => _members.AddRange(value); 
        }

        private readonly List<Member> _members = [];

        public Jamaat(string name, string code, Guid circuitId, Guid jamaatLedgerId, string createdBy)
        {
            Name = name;
            Code = code;
            CircuitId = circuitId;
            JamaatLedgerId = jamaatLedgerId;
            CreatedBy = createdBy;
        }

        public void SetJamaatLedger(JamaatLedger jamaatLedger)
        {
            JamaatLedger = jamaatLedger;
        }

        public void AddMembers(params Member[] members)
        {
            foreach (var member in members)
            {
                if(!_members.Any(m => m.ChandaNo == member.ChandaNo))
                {
                    _members.Add(member);
                }
            }
        }
    }
}
