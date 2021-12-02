using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace RMApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    
    public class SaleController : ControllerBase
    {
        private readonly IConfiguration _config;

        public SaleController(IConfiguration config)
        {
            _config = config;
        }
        [Authorize(Roles = "Cashier")]
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData(_config);
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


            SaleData data = new SaleData(_config);
            return data.GetSaleReport();
        }
    }
}
