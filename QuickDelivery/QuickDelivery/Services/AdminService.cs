using System.Collections.Generic;
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
            var addedProductIds = new List<long>();

            foreach (var product in products)
            {
                addedProductIds.Add(_productRepository.AddOrUpdateProduct(product));
            }

            return addedProductIds;
        }
    }
}
