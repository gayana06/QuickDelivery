using System;

namespace QuickDelivery.Entities
{
    public class PossibleDeliveryDates
    {
        public PossibleDeliveryDates(DateTime deliveryDate, bool isGreenDelivery)
        {
            DeliveryDate = deliveryDate;
            IsGreenDelivery = isGreenDelivery;
        }

        public DateTime DeliveryDate { get; set; }
        public bool IsGreenDelivery { get; set; }
    }
}
