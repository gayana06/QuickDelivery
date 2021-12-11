using System;
using System.Collections.Generic;
using System.Linq;
using QuickDelivery.Entities;
using QuickDelivery.Providers;
using Xunit;
using AutoFixture;
using QuickDelivery.Enums;
using QuickDelivery.Extensions;

namespace QuickDelivery.UnitTests.ProvidersTests
{
    //Point to discuss: Use of Theory and MemberData in case we need to test more complex product setups for the same method

    public class DeliveryDatesProviderTests : UnitTestBase
    {
        private readonly DeliveryDatesProvider _sut = new DeliveryDatesProvider();

        [Fact]
        public void GetPotentialDeliveryDates_Returns_Next_14_Days()
        {
            //act
            var result = _sut.GetPotentialDeliveryDates();

            //assert
            Assert.NotNull(result);
            Assert.Equal(14 ,result.Count);
            Assert.Equal(DateTime.Today.AddDays(1), result.First());
            Assert.Equal(DateTime.Today.AddDays(14), result.Last());
        }

        [Fact]
        public void FilterPotentialDeliveryDatesByProductDeliveryDays_Returns_EmptyList_If_No_PotentialDeliveryDates()
        {
            //arrange
            var products = GetAListOfProductsToBeDelivered();
            var potentialDeliveryDates = new List<DateTime>();

            //act
            var result = _sut.FilterPotentialDeliveryDatesByProductDeliveryDays(products, potentialDeliveryDates);

            //assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void FilterPotentialDeliveryDatesByProductDeliveryDays_Returns_PossibleDays_Successfully()
        {
            //arrange
            var products = GetAListOfProductsToBeDelivered();
            var potentialDeliveryDates = GetNext14DPotentialDeliveryDays();

            //act
            var result = _sut.FilterPotentialDeliveryDatesByProductDeliveryDays(products, potentialDeliveryDates);

            //assert
            Assert.NotNull(result);
            foreach (var date in result)
            {
                Assert.Equal(WeekDay.Tuesday.ToString(), date.DayOfWeek.ToString());
            }
        }

        [Fact]
        public void FilterPotentialDeliveryDatesByOrderedDaysInAdvanced_Returns_PossibleDays_Successfully()
        {
            //arrange
            var products = GetAListOfProductsToBeDelivered();
            var potentialDeliveryDates = GetNext14DPotentialDeliveryDays();
            

            //act
            var result = _sut.FilterPotentialDeliveryDatesByOrderedDaysInAdvanced(products, potentialDeliveryDates);

            //assert
            Assert.NotNull(result);
            var maxOrderedDaysInAdvance = products.Max(p => p.DaysInAdvance);
            for (var i = 1; i <= maxOrderedDaysInAdvance; i++)
            {
               Assert.DoesNotContain(DateTime.Today.AddDays(i), result);
            }
        }

        [Fact]
        public void FilterPotentialDeliveryDatesByTemporaryProductType_Returns_Same_InputPotentialDates_If_No_ProductOfType_Temporary()
        {
            //arrange
            var products = GetAListOfProductsToBeDelivered();
            var potentialDeliveryDates = GetNext14DPotentialDeliveryDays();


            //act
            var result = _sut.FilterPotentialDeliveryDatesByTemporaryProductType(products, potentialDeliveryDates);

            //assert
            Assert.NotNull(result);
            Assert.Equal(potentialDeliveryDates.Count, result.Count);
        }

        [Fact]
        public void FilterPotentialDeliveryDatesByTemporaryProductType_Returns_Dates_Until_Next_Sunday_If_ProductOfType_Temporary_Available()
        {
            //arrange
            var products = GetAListOfProductsToBeDelivered();
            products.Add(new Product(_fixture.Create<string>(), new List<WeekDay> { WeekDay.Monday, WeekDay.Tuesday }, ProductType.Temporary,0));
            var potentialDeliveryDates = GetNext14DPotentialDeliveryDays();


            //act
            var result = _sut.FilterPotentialDeliveryDatesByTemporaryProductType(products, potentialDeliveryDates);

            //assert
            Assert.NotNull(result);
            var nextSunday = DateTime.Today.Next(DayOfWeek.Sunday);
            var numberOfDaysTillNextSunday = (nextSunday - DateTime.Today).Days;
            Assert.Equal(numberOfDaysTillNextSunday, result.Count);
        }

        private List<Product> GetAListOfProductsToBeDelivered()
        {
            return new List<Product>
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
        }

        private List<DateTime> GetNext14DPotentialDeliveryDays()
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
