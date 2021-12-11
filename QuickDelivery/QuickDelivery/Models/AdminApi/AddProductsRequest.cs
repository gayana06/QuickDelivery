using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QuickDelivery.Enums;
using QuickDelivery.ValidationAttributes;

namespace QuickDelivery.Models.AdminApi
{
    public class AddProductsRequest
    {
        /// <summary>
        /// List of products
        /// </summary>
        /// <example></example>
        [Required]
        [EnsureMinimumElements(1)]
        public List<Product> Products { get; set; }

        public class Product
        {
            /// <summary>
            /// a string of max length of 200
            /// </summary>
            /// <example>productA</example>
            [Required]
            [StringLength(200)]
            public string Name { get; set; }

            /// <summary>
            /// List of delivery days
            /// </summary>
            /// <example>["Monday", "Tuesday"]</example>
            [Required]
            [ValidateEnumStringValueExists(typeof(WeekDay))]
            public List<string> DeliveryDays { get; set; }

            /// <summary>
            /// List of delivery days. Possible values are Normal, "External, Temporary
            /// </summary>
            /// <example>Normal</example>
            [Required]
            [ValidateEnumStringValueExists(typeof(ProductType))]
            public string ProductType { get; set; }

            /// <summary>
            /// Positive number or zero
            /// </summary>
            /// <example>3</example>
            [Required]
            [ExternalProductShouldOrder5DaysInAdvance("ProductType")]
            [Range(0, int.MaxValue)]
            public int DaysInAdvance { get; set; }
        }
    }
}
