using System.Collections.Generic;
using System.Linq;
using System.Net;
using Moq;
using QuickDelivery.Entities;
using QuickDelivery.Enums;
using QuickDelivery.Providers;
using QuickDelivery.Repositories;
using QuickDelivery.Services;
using Xunit;
using AutoFixture;
using QuickDelivery.Exceptions;

namespace QuickDelivery.UnitTests.ServicesTests
{
    //Note: I am not going to add the happy scenario, It will use the same style. I just wanted to show the exception case.
    public class CustomerServiceTests : UnitTestBase
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly CustomerService _sut;

        public CustomerServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            var deliveryDateProviderMock = new Mock<IDeliveryDatesProvider>();

            _sut = new CustomerService(_productRepositoryMock.Object, deliveryDateProviderMock.Object);
        }

        [Fact]
        public void FindPossibleDeliveryDatesOrThrow_Should_Throw_When_Provided_ProductId_NotAvailable_In_Database()
        {
            //arrange
            var allProducts = GetAllProducts();
            var productIdsOfItemsToBeDelivered = new List<long> {allProducts[0].Id, 1000};
            _productRepositoryMock.Setup(m => m.GetAllProductIds()).Returns(allProducts.Select(p => p.Id).ToList());

            //act
            var result = Assert.Throws<GenericApiException>(() => _sut.FindPossibleDeliveryDatesOrThrow(productIdsOfItemsToBeDelivered));

            //assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal("INVALID_PRODUCT_ID", result.ErrorCode);
        }

        private List<Product> GetAllProducts()
        {
            var productsTobeDelivered = GetAListOfProductsToBeDelivered();
            var newProduct = new Product(_fixture.Create<string>(),
                new List<WeekDay> {WeekDay.Monday, WeekDay.Tuesday}, ProductType.Temporary, 0);
            newProduct.Id = productsTobeDelivered.Max(p => p.Id) + 1;
            productsTobeDelivered.Add(newProduct);

            return productsTobeDelivered;
        }
    }
}
