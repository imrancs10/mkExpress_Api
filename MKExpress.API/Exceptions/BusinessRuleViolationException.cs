namespace MKExpress.API.Exceptions
{
    public class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string errorResponseType, string message, object? errors = null) :
            base(message)
        {
            ErrorResponseType = errorResponseType;
            Message = message;
            Errors = errors;
        }

        public string ErrorResponseType { get; }
        public override string Message { get; }

        public object? Errors { get; }
    }
}