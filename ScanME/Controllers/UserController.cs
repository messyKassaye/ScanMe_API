using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
       
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Json(new { me = "Me" });
        }
    }
}
