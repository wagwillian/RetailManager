﻿using RMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public interface IProductEndPoint
    {
        Task<List<ProductModel>> GetAll();
        Task CreateProduct(CreateProductModel model);
        Task UpdateProduct(ProductModel model);
        Task<ProductModel> GetProductById(int id);
    }
}