using RMDataManager.Library.Models;
using System.Collections.Generic;

namespace RMDataManager.Library.DataAccess
{
    public interface IProductData
    {
        void CreateProduct(ProductModel product);
        ProductModel GetProductById(int productId);
        List<ProductModel> GetProducts();
        void UpdateProduct(ProductModel product);
    }
}