using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record MemberDetials
    {
        public string Name { get; init; } = string.Empty;
        public string ChandaNo { get; init; } = string.Empty;
        public Guid JamaatId { get; init; }
        public string Email { get; init; } = string.Empty;
        public string Role { get; init; } = string.Empty;
    }
}
