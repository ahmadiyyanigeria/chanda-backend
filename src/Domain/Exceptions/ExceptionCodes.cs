namespace Domain.Exceptions
{
    public enum ExceptionCodes
    {
        MemberAlreadyExist = 1,
        MemberNotFound,
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
        InvoiceNotFound, 
        PaymentNotFound,
        InvalidFile,
        NotInYourJamaat,
        ReminderNotFound,
        ErrorPublishingEvent,
        ErrorSubscribingToEvent
    }
}
