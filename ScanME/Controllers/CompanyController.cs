using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScanME.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var company = new
            {
                name = "Company"
            };
            return Json(company);
        }
    }
}
