using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuickDelivery.Models.CustomerApi
{
    public class GetDeliveryDatesRequest
    {
        [Required]
        //Todo: Validate for postalcode
        public string PostalCode { get; set; }

        [Required]
        //[EnsureMinimumElements(1)]
        public List<long> ProductIds { get; set; }
    }
}
