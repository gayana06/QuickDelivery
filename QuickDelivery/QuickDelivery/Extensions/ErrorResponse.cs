using Guid = System.Guid;

namespace QuickDelivery.Extensions
{
    public class ErrorResponse
    {
        public ErrorResponse(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorReference = Guid.NewGuid();
        }

        public string ErrorCode { get; }
        public string ErrorMessage { get; }
        public Guid ErrorReference { get; }
    }
}
