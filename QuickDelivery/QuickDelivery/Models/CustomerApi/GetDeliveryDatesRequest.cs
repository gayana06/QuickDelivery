using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QuickDelivery.ValidationAttributes;

namespace QuickDelivery.Models.CustomerApi
{
    public class GetDeliveryDatesRequest
    {
        [Required]
        [StringLength(5)]
        public string PostalCode { get; set; }

        [Required]
        [EnsureMinimumElements(1)]
        public List<long> ProductIds { get; set; }
    }
}
