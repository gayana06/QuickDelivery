using System;
using System.Collections.Generic;
using System.Linq;
using QuickDelivery.Entities;
using QuickDelivery.Providers;
using QuickDelivery.Repositories;

namespace QuickDelivery.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDeliveryDatesProvider _deliveryDatesProvider;

        public CustomerService(IProductRepository productRepository, IDeliveryDatesProvider deliveryDatesProvider)
        {
            _productRepository = productRepository;
            _deliveryDatesProvider = deliveryDatesProvider;
        }

        public List<PossibleDeliveryDates> FindPossibleDeliveryDatesOrThrow(List<long> productIds)
        {
            var relevantProducts = GetRelevantProducts(productIds);

            var potentialDeliveryDates = _deliveryDatesProvider.GetPotentialDeliveryDates();
            potentialDeliveryDates = _deliveryDatesProvider.FilterPotentialDeliveryDatesByProductDeliveryDays(relevantProducts, potentialDeliveryDates);
            potentialDeliveryDates = _deliveryDatesProvider.FilterPotentialDeliveryDatesByOrderedDaysInAdvanced(relevantProducts, potentialDeliveryDates);
            potentialDeliveryDates = _deliveryDatesProvider.FilterPotentialDeliveryDatesByTemporaryProductType(relevantProducts, potentialDeliveryDates);

            var greenDeliveryDays = _deliveryDatesProvider.GetGreenDeliveryDates(potentialDeliveryDates);

            var finalizedDeliveryDates = _deliveryDatesProvider.SortByPriority(greenDeliveryDays, potentialDeliveryDates);

            return finalizedDeliveryDates;
        }

        private List<Product> GetRelevantProducts(List<long> productIds)
        {
            var allProductIds = _productRepository.GetAllProductIds();

            if (productIds.Any(productId => !allProductIds.Contains(productId)))
            {
                throw new Exception("List contains invalid product ids");
            }

            var selectedProducts = _productRepository.GetProductsByIds(productIds);

            return selectedProducts;
        }
    }
}
