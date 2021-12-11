using System;
using System.Diagnostics;

namespace QuickDelivery.Extensions
{
    public class ErrorResponse
    {
        public ErrorResponse(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorReference = Trace.CorrelationManager.ActivityId;
        }

        public string ErrorCode { get; }
        public string ErrorMessage { get; }
        public Guid ErrorReference { get; }
    }
}
