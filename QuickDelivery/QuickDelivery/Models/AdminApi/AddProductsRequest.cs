using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuickDelivery.Models.AdminApi
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
            //TODo:[ValidateEnumStringValueExists(typeof(JuridicalType))]
            public List<string> DeliveryDays { get; set; }

            [Required]
            // TODo:[ValidateEnumStringValueExists(typeof(JuridicalType))]
            //TODo:ExternalProduct  DaysInAdvance 5
            public string ProductType { get; set; }

            [Required]
            public int DaysInAdvance { get; set; }
        }
    }
}
