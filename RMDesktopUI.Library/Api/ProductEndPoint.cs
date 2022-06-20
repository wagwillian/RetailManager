using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Api
{
    public class ProductEndPoint : IProductEndPoint
    {
        private IAPIHelper _apiHelper;

        public ProductEndPoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
                

        public async Task<List<ProductModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Product"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ProductModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task CreateProduct(CreateProductModel model)
        {
            var data = new
            {
                model.ProductName,
                model.Description,
                model.RetailPrice,
                model.QuantityInStock,
                model.IsTaxable,
                model.ProductImage

            };

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Product/Register", data))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }


        }        
    }
}
