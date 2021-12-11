using System;
using System.Collections.Generic;
using System.Linq;
using QuickDelivery.Entities;
using QuickDelivery.Enums;
using QuickDelivery.Extensions;

namespace QuickDelivery.Providers
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

            if (!potentialDeliveryDates.Any())
            {
                return possibleDeliveryDates;
            }

            var intersectingDeliveryDays = FindIntersectingWeekDaysForProducts(products);

            foreach (var potentialDeliveryDate in potentialDeliveryDates)
            {
                foreach (var possibleDay in intersectingDeliveryDays)
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
            var possibleDeliveryDates = new List<DateTime>();

            if (!potentialDeliveryDates.Any())
            {
                return possibleDeliveryDates;
            }

            var maxOrderedDaysInAdvance = products.Max(p => p.DaysInAdvance);  //Possible point of discussion: for Product type External
            possibleDeliveryDates = potentialDeliveryDates.Where(potentialDeliveryDate => (potentialDeliveryDate - DateTime.Today).Days > maxOrderedDaysInAdvance).ToList();

            return possibleDeliveryDates;
        }

        public List<DateTime> FilterPotentialDeliveryDatesByTemporaryProductType(List<Product> products, List<DateTime> potentialDeliveryDates)
        {
            var possibleDeliveryDates = new List<DateTime>();

            if (!potentialDeliveryDates.Any())
            {
                return possibleDeliveryDates;
            }

            var hasProductsOfTypeTemporary = products.Exists(p => p.ProductType == ProductType.Temporary);
            if (!hasProductsOfTypeTemporary)
            {
                return potentialDeliveryDates;
            }

            possibleDeliveryDates = PossibleDeliveryDatesUntilNextSunday(potentialDeliveryDates);

            return possibleDeliveryDates;
        }

        public List<DateTime> GetGreenDeliveryDates(List<DateTime> potentialDeliveryDates)
        {
            var possibleDeliveryDates = new List<DateTime>();

            if (!potentialDeliveryDates.Any())
            {
                return possibleDeliveryDates;
            }

            possibleDeliveryDates = PossibleDeliveryDatesUntilNextSunday(potentialDeliveryDates);

            return possibleDeliveryDates;
        }

        public List<PossibleDeliveryDates> SortByPriority(List<DateTime> greenDeliveryDates, List<DateTime> potentialDeliveryDates)
        {
            var possibleDeliveryDates = new List<PossibleDeliveryDates>();
            potentialDeliveryDates = potentialDeliveryDates.OrderBy(potentialDeliveryDate => potentialDeliveryDate).ToList();

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

            return possibleDeliveryDates;
        }

        private IEnumerable<WeekDay> FindIntersectingWeekDaysForProducts(List<Product> products)
        {
            var intersectingDeliveryDays = products[0].DeliveryDays.AsEnumerable();
            for (var i = 1; i < products.Count; i++)
            {
                intersectingDeliveryDays = intersectingDeliveryDays.Intersect(products[i].DeliveryDays);

                if (!intersectingDeliveryDays.Any())
                {
                    break;
                }
            }

            return intersectingDeliveryDays;
        }
    }
}
