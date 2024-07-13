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
        private readonly List<MemberRole> _userRoles;

        public Role(string name, string description, string createdBy)
        {
            Name = name;
            Description = description;
            CreatedBy = createdBy;
            _userRoles = new List<MemberRole>();
        }

        public IReadOnlyList<MemberRole> UserRoles
        {
            get => _userRoles.AsReadOnly();
            set => _userRoles.AddRange(value);
        }
    }
}
