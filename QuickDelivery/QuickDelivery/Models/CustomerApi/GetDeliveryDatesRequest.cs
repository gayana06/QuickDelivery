using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QuickDelivery.ValidationAttributes;

namespace QuickDelivery.Models.CustomerApi
{
    public class GetDeliveryDatesRequest
    {
        /// <summary>
        /// postal code of 5 characters
        /// </summary>
        /// <example>12344</example>
        [Required]
        [StringLength(5)]
        public string PostalCode { get; set; }

        /// <summary>
        /// list of product ids
        /// </summary>
        /// <example>1, 2</example>
        [Required]
        [EnsureMinimumElements(1)]
        public List<long> ProductIds { get; set; }
    }
}
