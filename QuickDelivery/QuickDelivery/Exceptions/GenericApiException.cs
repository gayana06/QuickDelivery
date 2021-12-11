using System;
using System.Net;

namespace QuickDelivery.Exceptions
{
    public class GenericApiException : Exception
    {
        public GenericApiException(HttpStatusCode statusCode, string errorCode, string detailedErrorMessage)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            DetailedErrorMessage = detailedErrorMessage;
        }

        public HttpStatusCode StatusCode { get; }
        public string ErrorCode { get; }
        public string DetailedErrorMessage { get; }
    }
}
