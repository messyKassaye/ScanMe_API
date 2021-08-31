using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScanME.Contexts;
using ScanME.DTO;
using ScanME.Models;
using ScanME.Services.Interfaces;
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

        public IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
       
        [HttpGet("me")]
        public IActionResult Me()
        {
            int loggedUserId = (int)HttpContext.Items["UserId"];
            var response =  _service.Me(loggedUserId);

            return Json(response);
        }
    }
}
