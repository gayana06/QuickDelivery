using System.Collections.Generic;
using QuickDelivery.Enums;

namespace QuickDelivery.Entities
{
    public class Product
    {
        public Product(string name, List<WeekDay> deliveryDays, ProductType productType, int daysInAdvance)
        {
            Name = name;
            DeliveryDays = deliveryDays;
            ProductType = productType;
            DaysInAdvance = daysInAdvance;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public List<WeekDay> DeliveryDays { get; set; }
        public ProductType ProductType { get; set; }
        public int DaysInAdvance { get; set; }
    }
}
