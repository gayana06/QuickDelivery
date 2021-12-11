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
        
        public long AddProduct(Product product)
        {
            product.Id = GetNewProductId();
            _productDictionary.Add(product.Id, product);
            return product.Id;
        }

        public List<long> GetAllProductIds()
        {
            return _productDictionary.Keys.ToList();
        }

        public List<string> GetAllProductNames()
        {
            return _productDictionary.Values.Select(product => product.Name).ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _productDictionary.Values.ToList();
        }

        public List<Product> GetProductsByIds(List<long> productIds)
        {
            var selectedProducts = new List<Product>();
           
            foreach (var productId in productIds)
            {
                selectedProducts.Add(_productDictionary[productId]);   
            }

            return selectedProducts;
        }

        private long GetNewProductId()
        {
            return _productDictionary.Count == 0 ? 1 : _productDictionary.Keys.Max() + 1;
        }
    }
}
