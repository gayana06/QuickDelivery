using System;
using System.Collections.Generic;
using System.Linq;
using QuickDelivery.Entities;
using QuickDelivery.Repositories;

namespace QuickDelivery.Services
{
    public class AdminService : IAdminService
    {
        private readonly IProductRepository _productRepository;

        public AdminService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<long> AddProducts(List<Product> products)
        {
            ValidateForExistingProduct(products);

            var addedProductIds = new List<long>();

            foreach (var product in products)
            {
                addedProductIds.Add(_productRepository.AddProduct(product));
            }

            return addedProductIds;
        }

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        private void ValidateForExistingProduct(List<Product> products)
        {
            var productNames = _productRepository.GetAllProductNames();

            var alreadyExistingProducts = products.Where(product => productNames.Contains(product.Name, StringComparer.InvariantCultureIgnoreCase));
            if (!alreadyExistingProducts.Any())
            {
                return;
            }

            throw new Exception($"Product(s) '{string.Join(',', alreadyExistingProducts)}' already exists");
        }
    }
}
