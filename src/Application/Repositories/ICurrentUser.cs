using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ICurrentUser
    {
        MemberDetials? GetMemberDetails();
        string GetUserId();
        string GetUserNameAndChandaNo();
    }
}
