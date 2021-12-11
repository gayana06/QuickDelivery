using System.Collections.Generic;
using QuickDelivery.Entities;

namespace QuickDelivery.Services
{
    public interface ICustomerService
    {
        //Request response model, possible discussion point.
        List<PossibleDeliveryDates> FindPossibleDeliveryDatesOrThrow(List<long> productIds);
    }
}
