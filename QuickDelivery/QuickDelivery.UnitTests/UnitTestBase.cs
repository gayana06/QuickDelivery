using System;
using System.Collections.Generic;
using AutoFixture;
using QuickDelivery.Entities;
using QuickDelivery.Enums;

namespace QuickDelivery.UnitTests
{
    public class UnitTestBase
    {
        protected readonly Fixture _fixture;

        protected UnitTestBase()
        {
            _fixture = new Fixture();
        }

        protected List<Product> GetAListOfProductsToBeDelivered()
        {
            var products = new List<Product>
            {
                new Product(
                    _fixture.Create<string>()
                    , new List<WeekDay> {WeekDay.Monday, WeekDay.Tuesday}
                    , ProductType.Normal
                    , 1),
                new Product(
                    _fixture.Create<string>()
                    , new List<WeekDay> {WeekDay.Tuesday, WeekDay.Wednesday}
                    , ProductType.Normal
                    , 2),
                new Product(
                    _fixture.Create<string>()
                    , new List<WeekDay> {WeekDay.Tuesday, WeekDay.Friday}
                    , ProductType.Normal
                    , 3)
            };

            var i = 0;
            products.ForEach(p => p.Id = i++);

            return products;
        }

        protected List<DateTime> GetNext14DPotentialDeliveryDays()
        {
            var potentialDeliveryDates = new List<DateTime>();

            for (var i = 1; i <= 14; i++)
            {
                potentialDeliveryDates.Add(DateTime.Today.AddDays(i));
            }

            return potentialDeliveryDates;
        }
    }
}
