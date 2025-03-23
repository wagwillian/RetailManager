using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _sql;

        public ProductData(ISqlDataAccess sql)
        {            
            _sql = sql;
        }
        public List<ProductModel> GetProducts()
        {

            var output = _sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");

            return output;
        }

        public ProductModel GetProductById(int productId)
        {

            var output = _sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "RMData").FirstOrDefault();

            return output;
        }


        public void CreateProduct(ProductModel product)
        {
            _sql.SaveData("dbo.spProduct_Insert", new {product.ProductName, product.Description, product.RetailPrice, product.QuantityInStock, product.IsTaxable, product.ProductImage }, "RMData");
        }

        public void UpdateProduct(ProductModel product)
        {
            _sql.SaveData("dbo.spProduct_Update", product, "RMData");
        }
    }
}
