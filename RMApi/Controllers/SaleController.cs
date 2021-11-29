using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;

namespace RMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  //RequestContext.Principal.Identity.GetUserId(); .NetFramework

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
