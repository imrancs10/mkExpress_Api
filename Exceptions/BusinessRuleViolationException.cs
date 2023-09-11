using System;
namespace MKExpress.API.Exceptions
{
    public class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string errorResponseType, string message, object? errors = null) :
            base(message)
        {
            ErrorResponseType = errorResponseType;
            Errors = errors;
        }

        public string ErrorResponseType { get; }

        public object? Errors { get; }
    }
}