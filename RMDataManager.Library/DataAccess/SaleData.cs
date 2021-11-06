﻿using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            //Make this SOLID/DRY/Better
            // Start filling in the models we will save to the database
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData products = new ProductData();
            var taxRate = ConfigHelper.GetTaxRate()/100;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                //Get The Information about this product , Fill in the avaliable information
                var productInfo = products.GetProductById(detail.ProductId);

                if (productInfo == null)
                {
                    throw new Exception($"The product Id of {detail.ProductId} could not be found in the database.");
                }

                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }

                details.Add(detail);
            }
            //Create the sale model
            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };
            sale.Total = sale.SubTotal + sale.Tax;
            //save the sale model
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spSale_Insert", sale, "RMData");

            //get the Id form the sale model
            sale.Id = sql.LoadData<int, dynamic>("spSale_LookUp", new {sale.CashierId, sale.SaleDate }, "RMData").FirstOrDefault();

            //Finish filling in the sale detail models

            foreach (var item in details)
            {
                item.SaleId = sale.Id;
                //save the sale detai models
                sql.SaveData("dbo.spSaleDetail_Insert", item, "RMData");
            }           

        }
        
        
    }
}
