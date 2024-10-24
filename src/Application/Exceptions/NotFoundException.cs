﻿namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public int HttpStatusCode { get; init; }
        public object? Error { get; init; }
        public string ErrorCode { get; init; } = default!;

        public NotFoundException(string message, string errorCode, int statusCode, object? error = null) : base(message)
        {
            HttpStatusCode = statusCode;
            Error = error;
            ErrorCode = errorCode;
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        { }

    }
}
