using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuickDelivery.Exceptions;
using QuickDelivery.Extensions;

namespace QuickDelivery.Filters
{
    public class HttpResponseExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var status = HttpStatusCode.InternalServerError;
            var errorCode = CommonApiErrorCodes.SystemError;
            var errorMessage = context.Exception.Message;

            if (context.Exception is GenericApiException genericApiException)
            {
                status = genericApiException.StatusCode;
                errorCode = genericApiException.ErrorCode;
                errorMessage = genericApiException.DetailedErrorMessage;
            }

            context.ExceptionHandled = true;
            var response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            context.Result = new ObjectResult(new ErrorResponse(errorCode, errorMessage));
        }
    }
}
