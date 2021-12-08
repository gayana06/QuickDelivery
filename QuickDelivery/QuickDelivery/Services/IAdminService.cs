using System.Collections.Generic;
using QuickDelivery.Entities;

namespace QuickDelivery.Services
{
    public interface IAdminService
    {
        List<long> AddProducts(List<Product> products);
    }
}
