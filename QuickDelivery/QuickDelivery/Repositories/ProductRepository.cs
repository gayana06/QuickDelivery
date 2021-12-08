using System;
using System.Collections.Generic;
using System.Linq;
using QuickDelivery.Entities;

namespace QuickDelivery.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private readonly Dictionary<long, Product> _productDictionary;

        public ProductRepository()
        {
            _productDictionary = new Dictionary<long, Product>();
        }
        
        public long AddOrUpdateProduct(Product product)
        {
            var (productExists, existingProductId) = CheckProductAlreadyExistsAndFind(product.Name);

            if (!productExists)
            {
                var newProductId = GetNewProductId();
                _productDictionary.Add(newProductId, product);
                return newProductId;
            }
            
            _productDictionary[existingProductId.Value] = product;
            return existingProductId.Value;
        }

        public List<Product> GetAllProducts()
        {
            return _productDictionary.Values.ToList();
        }

        private long GetNewProductId()
        {
            return _productDictionary.Count == 0 ? 1 : _productDictionary.Keys.Max() + 1;
        }

        private (bool, long?) CheckProductAlreadyExistsAndFind(string productName)
        {
            var productExists = false;
            long? productId = null;

            foreach (var record in _productDictionary)
            {
                if (!record.Value.Name.Equals(productName, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                productExists = true;
                productId = record.Key;
                break;
            }

            return (productExists, productId);
        }
    }
}
