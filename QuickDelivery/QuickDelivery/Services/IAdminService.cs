using System.Collections.Generic;
using QuickDelivery.Entities;

namespace QuickDelivery.Services
{
    public interface IAdminService
    {
        //Request response model, possible discussion point.
        List<long> AddProducts(List<Product> products);
        List<Product> GetAllProducts();
    }
}
