using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuickDelivery.Models
{
    public class AddProductsRequest
    {
        [Required]
        //[EnsureMinimumElements(1)]
        public List<Product> Products { get; set; }

        public class Product
        {
            [Required]
            public string Name { get; set; }

            [Required]
            //[ValidateEnumStringValueExists(typeof(JuridicalType))]
            public List<string> DeliveryDays { get; set; }

            [Required]
            //[ValidateEnumStringValueExists(typeof(JuridicalType))]
            public string ProductType { get; set; }

            [Required]
            public int DaysInAdvance { get; set; }
        }
    }
}
