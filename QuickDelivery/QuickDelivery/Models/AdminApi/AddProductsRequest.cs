using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QuickDelivery.Enums;
using QuickDelivery.ValidationAttributes;

namespace QuickDelivery.Models.AdminApi
{
    public class AddProductsRequest
    {
        [Required]
        [EnsureMinimumElements(1)]
        public List<Product> Products { get; set; }

        public class Product
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [ValidateEnumStringValueExists(typeof(WeekDay))]
            public List<string> DeliveryDays { get; set; }

            [Required]
            [ValidateEnumStringValueExists(typeof(ProductType))]
            public string ProductType { get; set; }

            [Required]
            [ExternalProductShouldOrder5DaysInAdvance("ProductType")]
            [Range(0, int.MaxValue)]
            public int DaysInAdvance { get; set; }
        }
    }
}
