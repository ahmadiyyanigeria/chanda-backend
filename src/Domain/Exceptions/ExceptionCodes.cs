using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public enum ExceptionCodes
    {
        MemberAlreadyExist = 1,
        MemberNotFound,
        InvoiceNotFound,
        UserNotLoggedIn,
        Unauthorized,
        InvalidRole,
        RoleNotFound,
        UserAlreadyExists,
        NoRoleSelected,
        InvalidCredentials,
        JamaatNotFound,
        CircuitNotFound,
        NoInvoiceItemSelected,
        InvalidJamaat,
        NoValidChandaTypeSelected,
        ChandaTypeNotFound,
        InvoiceNotFound
    }
}
