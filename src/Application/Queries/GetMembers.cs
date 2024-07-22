using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetMembers
    {


        public record MemberResponse(Guid Id, string ChandaNo, string Name, string Email, string PhoneNo, string JamaatName, string CircuitName, IReadOnlyList<string> Roles);
    }
}
