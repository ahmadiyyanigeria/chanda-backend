using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Member: BaseEntity
    {
        public string ChandaNo { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNo { get; private set; }
        public Guid JamaatId { get; private set; }
        public Guid MemberLedgerId { get; private set; }
        public Jamaat Jamaat { get; private set; } = default!;
        public MemberLedger MemberLedger { get; private set; } = default!;
        public IReadOnlyList<MemberRole> MemberRoles
        {
            get => _memberRoles.AsReadOnly();
            private set => _memberRoles.AddRange(value);
        }

        private readonly List<MemberRole> _memberRoles = [];

        public Member(string chandaNo, string name, string email, string phoneNo, Guid jamaatId, Guid memberLedgerId, string createdBy)
        {
            ChandaNo = chandaNo;
            Name = name;
            Email = email;
            PhoneNo = phoneNo;
            JamaatId = jamaatId;
            MemberLedgerId = memberLedgerId;
            CreatedBy = createdBy;
        }

        public void AddMemberRole(params MemberRole[] memberRoles) 
        {
            foreach (var meberRole in memberRoles)
            {
                if (!_memberRoles.Any(mr => mr.RoleId == meberRole.RoleId))
                {
                    _memberRoles.Add(meberRole);
                }
            }
        }
    }
}
