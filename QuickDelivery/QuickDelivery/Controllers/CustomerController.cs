using Microsoft.AspNetCore.Mvc;
using QuickDelivery.Models.CustomerApi;

namespace QuickDelivery.Controllers
{
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("customerapi/products/delivery")]
        public IActionResult PostAsync([FromQuery] GetDeliveryDatesRequest getDeliveryDatesRequest)
        {


            return Ok();
        }
    }
}
