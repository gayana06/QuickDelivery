using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using QuickDelivery.Models.CustomerApi;
using QuickDelivery.Services;

namespace QuickDelivery.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("customerapi/products/delivery")]
        public IActionResult PostAsync([FromQuery] GetDeliveryDatesRequest getDeliveryDatesRequest)
        {
            var possibleDeliveryDates = _customerService.FindPossibleDeliveryDatesOrThrow(getDeliveryDatesRequest.ProductIds);

            var response = new List<DeliveryDateOption>();

            foreach (var possibleDeliveryDate in possibleDeliveryDates)
            {
                response.Add(new DeliveryDateOption(getDeliveryDatesRequest.PostalCode, possibleDeliveryDate.DeliveryDate, possibleDeliveryDate.IsGreenDelivery));
            }
            
            return Ok(response);
        }
    }
}
