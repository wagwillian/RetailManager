using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using RMApi.Data;
using System.Security.Claims;
using RMApi.Models;

namespace RMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductController : ControllerBase
    {
        private readonly IProductData _productData;              


        public ProductController(IProductData productData)
        {
            _productData = productData;
        }
        [HttpGet]
        [Authorize(Roles = "Cashier")]
        public List<ProductModel> Get()
        {
            
            var products = _productData.GetProducts();
            return products;

        }

        public record ProductRegistrationModel(
            int Id,
            string ProductName,
            string Description,
            double RetailPrice,
            int QuantityInStock,
            bool IsTaxable,
            string ProductImage);

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Register")]
        public async Task<IActionResult> ProductRegister(ProductRegistrationModel product)
        {

            ProductModel p = new()
            {                                
                ProductName = product.ProductName,
                Description = product.Description,
                RetailPrice = (decimal)product.RetailPrice,
                QuantityInStock = product.QuantityInStock,
                IsTaxable = product.IsTaxable,
                ProductImage = product.ProductImage
            };

            _productData.CreateProduct(p);
            return Ok(p);
        }

        [HttpPut]
        [Authorize]
        [Route("Update")]
        public async Task<IActionResult> ProductPut (ProductRegistrationModel product)
        {
            ProductModel p = new()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                RetailPrice = (decimal)product.RetailPrice,
                QuantityInStock = product.QuantityInStock,
                IsTaxable = product.IsTaxable,
                ProductImage = product.ProductImage
            };
            _productData.UpdateProduct(p);
            return Ok(p);
        }


    }
}
