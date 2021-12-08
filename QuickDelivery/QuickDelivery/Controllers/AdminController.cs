using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuickDelivery.Entities;
using QuickDelivery.Enums;
using QuickDelivery.Models;
using QuickDelivery.Services;

namespace QuickDelivery.Controllers
{
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        [Route("adminapi/products")]
        public IActionResult PostAsync([FromBody] AddProductsRequest addProductsRequest)
        {
            var mappedProducts = new List<Product>();
            
            foreach (var product in addProductsRequest.Products)
            {
                var mappedDeliveryDays = product.DeliveryDays.Select(x => (WeekDay) Enum.Parse(typeof(WeekDay), x)).ToList();
                var mappedProductType = (ProductType) Enum.Parse(typeof(ProductType), product.ProductType);
                var mappedProduct = new Product(product.Name, mappedDeliveryDays, mappedProductType, product.DaysInAdvance);

                mappedProducts.Add(mappedProduct);
            }

            var newlyAddedProductIds = _adminService.AddProducts(mappedProducts);

            return Ok(newlyAddedProductIds);
        }

        [HttpGet]
        [Route("adminapi/products")]
        public string GetAllProducts()
        {
            return "Hi";
        }
    }
}
