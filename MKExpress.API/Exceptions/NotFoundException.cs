using System;

namespace MKExpress.API.Exceptions
{

    public class NotFoundException : Exception
    {
        public NotFoundException(string errorResponseType = "ObjectNotFound", string message = "Object not found") :
            base(message)
        {
            ErrorResponseType = errorResponseType;
        }

        public string ErrorResponseType { get; set; }
    }
}