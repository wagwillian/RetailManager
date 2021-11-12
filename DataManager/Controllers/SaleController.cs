using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;


namespace RMDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        [Authorize(Roles ="Cashier")]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();

            data.SaveSale(sale, userId);
        }
        [Authorize(Roles = "Admin,Manager")]
        [Route("GetSaleReport")]
        public List<SaleReportModel> GetSalesReportModels()
        {
           //if (RequestContext.Principal.IsInRole("Admin"))
           // {
           //     //admin logic
           // }
           //else if (RequestContext.Principal.IsInRole("Manager"))
           // {
           //     //Manager logic
           // }


            SaleData data = new SaleData();
            return data.GetSaleReport();
        }
    }
}
