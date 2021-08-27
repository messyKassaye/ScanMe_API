using Microsoft.AspNetCore.Mvc;
using ScanME.Contexts;
using ScanME.DTO;
using ScanME.UnitOfWorks;
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

        public ApplicationUnitOfWork _unitOfWork;

        public UserController(ApplicationDbContext context)
        {
            _unitOfWork = new ApplicationUnitOfWork(context);
        }
       
        [HttpGet("me")]
        public IActionResult Me()
        {
            int UserId = (int)HttpContext.Items["UserId"];
            var user = _unitOfWork.UserRepository.Show(UserId);
            var userDTP= new UserDTO()
            {
                FullName=user.FullName,
                Email=user.Email,
                Phone=user.Phone
            };

            return Json(userDTP);
        }
    }
}
