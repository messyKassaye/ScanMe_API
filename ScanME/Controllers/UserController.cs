using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScanME.Contexts;
using ScanME.DTO;
using ScanME.Models;
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

        public ApplicationUnitOfWork<Users> _unitOfWork;
        private readonly IMapper _mapper;

        public UserController(ApplicationDbContext context,IMapper mapper)
        {
            _unitOfWork = new ApplicationUnitOfWork<Users>(context);
            _mapper = mapper;
        }
       
        [HttpGet("me")]
        public IActionResult Me()
        {
            int UserId = (int)HttpContext.Items["UserId"];
            Users user = _unitOfWork.ModelRepository.Show(UserId);
            var resources = _mapper.Map<Users, UserDTO>(user);
            return Json(resources);
        }
    }
}
