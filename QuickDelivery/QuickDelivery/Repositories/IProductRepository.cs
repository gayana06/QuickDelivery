using System.Collections.Generic;
using QuickDelivery.Entities;

namespace QuickDelivery.Repositories
{
    public interface IProductRepository
    {
        long AddProduct(Product product);
        List<long> GetAllProductIds();
        List<string> GetAllProductNames();
        List<Product> GetAllProducts();
        List<Product> GetProductsByIds(List<long> productIds);
    }
}
