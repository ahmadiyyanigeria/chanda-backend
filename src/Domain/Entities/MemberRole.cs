using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MemberRole: BaseEntity
    {
        public Guid MemberId { get; private set; }
        public Guid RoleId { get; private set; }
        public Member Member { get; private set; } = default!;
        public Role Role { get; private set; } = default!;

        public MemberRole(Guid memberId, Guid roleId, string createdBy)
        {
            MemberId = memberId;
            RoleId = roleId;
            CreatedBy = createdBy;
        }
    }
}
