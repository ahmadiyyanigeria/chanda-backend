using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        private readonly List<MemberRole> _memberRoles = [];

        public Role(string name, string description, string createdBy)
        {
            Name = name;
            Description = description;
            CreatedBy = createdBy;
        }

        public IReadOnlyList<MemberRole> MemberRoles
        {
            get => _memberRoles.AsReadOnly();
            private set => _memberRoles.AddRange(value);
        }
    }
}
