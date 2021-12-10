using System;
using System.Collections.Generic;
using QuickDelivery.Entities;

namespace QuickDelivery.Helpers
{
    public interface IDeliveryDatesProvider
    {
        List<DateTime> GetPotentialDeliveryDates();
        List<DateTime> FilterPotentialDeliveryDatesByProductDeliveryDays(List<Product> products, List<DateTime> potentialDeliveryDates);
        List<DateTime> FilterPotentialDeliveryDatesByOrderedDaysInAdvanced(List<Product> products, List<DateTime> potentialDeliveryDates);
        List<DateTime> FilterPotentialDeliveryDatesByTemporaryProductType(List<Product> products, List<DateTime> potentialDeliveryDates);
        List<DateTime> GetGreenDeliveryDates(List<DateTime> potentialDeliveryDates);
        List<PossibleDeliveryDates> SortByPriority(List<DateTime> greenDeliveryDates, List<DateTime> potentialDeliveryDates);
    }
}
