using System.Collections.Generic;

namespace QuickDelivery.Models.AdminApi
{
    public class GetAllProductsResponse
    {
        public GetAllProductsResponse()
        {
            Products = new List<Product>();
        }

        public List<Product> Products { get; }

        public class Product
        {
            public Product(long id, string name, List<string> deliveryDays, string productType, int daysInAdvance)
            {
                Id = id;
                Name = name;
                DeliveryDays = deliveryDays;
                ProductType = productType;
                DaysInAdvance = daysInAdvance;
            }

            public long Id { get; set; }
            public string Name { get; set; }
            public List<string> DeliveryDays { get; set; }
            public string ProductType { get; set; }
            public int DaysInAdvance { get; set; }
        }
    }
}
