using System.Collections.Generic;
using QuickDelivery.Entities;

namespace QuickDelivery.Repositories
{
    public interface IProductRepository
    {
        long AddOrUpdateProduct(Product product);
        List<Product> GetAllProducts();
    }
}
