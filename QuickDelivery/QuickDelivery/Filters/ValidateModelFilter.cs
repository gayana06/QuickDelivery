using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuickDelivery.Extensions;

namespace QuickDelivery.Filters
{
    public class ValidateModelFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Any(kv => kv.Value == null))
            {
                var errorResponse = new ErrorResponse(CommonApiErrorCodes.ArgumentsCannotBeNull, null);
                context.Result = new ObjectResult(errorResponse) { StatusCode = (int)HttpStatusCode.BadRequest };
            }

            if (!context.ModelState.IsValid)
            {
                var sb = new StringBuilder();
                foreach (var state in context.ModelState)
                {
                    sb.Append(" Property: " + state.Key + ",");
                    foreach (var error in state.Value.Errors)
                    {
                        sb.Append(" Message: " + error.ErrorMessage);
                    }
                }

                var errorResponse = new ErrorResponse(CommonApiErrorCodes.InvalidInput, sb.ToString());
                context.Result = new ObjectResult(errorResponse) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
        }
    }
}
