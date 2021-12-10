using System;
using System.Collections.Generic;
using System.Linq;
using QuickDelivery.Entities;
using QuickDelivery.Enums;

namespace QuickDelivery.Helpers
{
    internal class DeliveryDatesProvider : IDeliveryDatesProvider
    {
        private const int NextPotentialNumberOfDates = 14; //Possible to take from config if necessary
        
        public List<DateTime> GetPotentialDeliveryDates()
        {
            var potentialDeliveryDates = new List<DateTime>();
            
            for (var i = 1; i <= NextPotentialNumberOfDates; i++)
            {
                potentialDeliveryDates.Add(DateTime.Today.AddDays(i));
            }

            return potentialDeliveryDates;
        }

        public List<DateTime> FilterPotentialDeliveryDatesByProductDeliveryDays(List<Product> products, List<DateTime> potentialDeliveryDates)
        {
            var possibleDeliveryDates = new List<DateTime>();

            var possibleDeliveryDaysForTheFirstProductInList = products[0].DeliveryDays.AsEnumerable();
            for (var i = 1; i < products.Count; i++)
            {
                if (!possibleDeliveryDaysForTheFirstProductInList.Any())
                {
                    return possibleDeliveryDates;
                }

                possibleDeliveryDaysForTheFirstProductInList = possibleDeliveryDaysForTheFirstProductInList.Intersect(products[i].DeliveryDays);
            }

            foreach (var potentialDeliveryDate in potentialDeliveryDates)
            {
                foreach (var possibleDay in possibleDeliveryDaysForTheFirstProductInList)
                {
                    if (possibleDay.ToString().Equals(potentialDeliveryDate.DayOfWeek.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        possibleDeliveryDates.Add(potentialDeliveryDate);
                    }
                }
            }

            return possibleDeliveryDates;
        }

        public List<DateTime> FilterPotentialDeliveryDatesByOrderedDaysInAdvanced(List<Product> products, List<DateTime> potentialDeliveryDates)
        {
            var maxOrderedDaysInAdvanced = products.Max(p => p.DaysInAdvance);
            var hasProductsOfTypeExternal = products.Exists(p => p.ProductType == ProductType.External);
            if (hasProductsOfTypeExternal && maxOrderedDaysInAdvanced < 5)
            {
                maxOrderedDaysInAdvanced = 5;
            }

            var possibleDeliveryDates = potentialDeliveryDates.Where(potentialDeliveryDate => (potentialDeliveryDate - DateTime.Today).Days > maxOrderedDaysInAdvanced).ToList();

            return possibleDeliveryDates;
        }

        public List<DateTime> FilterPotentialDeliveryDatesByTemporaryProductType(List<Product> products, List<DateTime> potentialDeliveryDates)
        {
            var hasProductsOfTypeTemporary = products.Exists(p => p.ProductType == ProductType.Temporary);
            if (!hasProductsOfTypeTemporary)
            {
                return potentialDeliveryDates;
            }

            var possibleDeliveryDates = PossibleDeliveryDatesUntilNextSunday(potentialDeliveryDates);

            return possibleDeliveryDates;
        }

        public List<DateTime> GetGreenDeliveryDates(List<DateTime> potentialDeliveryDates)
        {
            var possibleDeliveryDates = PossibleDeliveryDatesUntilNextSunday(potentialDeliveryDates);

            return possibleDeliveryDates;
        }

        public List<PossibleDeliveryDates> SortByPriority(List<DateTime> greenDeliveryDates, List<DateTime> potentialDeliveryDates)
        {
            var possibleDeliveryDates = new List<PossibleDeliveryDates>();

            foreach (var potentialDeliveryDate in potentialDeliveryDates)
            {
                possibleDeliveryDates.Add(new PossibleDeliveryDates(potentialDeliveryDate, greenDeliveryDates.Contains(potentialDeliveryDate)));
            }

            return possibleDeliveryDates;
        }

        private List<DateTime> PossibleDeliveryDatesUntilNextSunday(List<DateTime> potentialDeliveryDates)
        {
            var nextSunday = DateTime.Today.Next(DayOfWeek.Sunday);

            var possibleDeliveryDates = potentialDeliveryDates
                .Where(potentialDeliveryDate => potentialDeliveryDate <= nextSunday).ToList();

            return possibleDeliveryDates
        }

    }
}
