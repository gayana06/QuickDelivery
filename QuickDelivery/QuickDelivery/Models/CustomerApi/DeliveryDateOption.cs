using System;
using Newtonsoft.Json;

namespace QuickDelivery.Models.CustomerApi
{
    public class DeliveryDateOption //Point to discuss: Not adhere to the convension.
    {
        public DeliveryDateOption(string postalCode, DateTime deliveryDate, bool isGreenDelivery)
        {
            PostalCode = postalCode;
            DeliveryDate = deliveryDate;
            IsGreenDelivery = isGreenDelivery;
        }


        [JsonProperty("postalCode")] //Point of discussion: use of CamelCase resolver
        public string PostalCode { get; }

        [JsonProperty("deliveryDate")] //Point of discussion: use of CamelCase resolver
        public DateTime DeliveryDate { get; }

        [JsonProperty("isGreenDelivery")] //Point of discussion: use of CamelCase resolver
        public bool IsGreenDelivery { get; }
    }
}
