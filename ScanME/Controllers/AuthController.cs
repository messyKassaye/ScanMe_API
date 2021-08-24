﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ScanME.Models;
using ScanME.Repository.Interfaces;
using ScanME.Services.Interfaces;
using System.Threading.Tasks;

namespace ScanME.Controllers
{

    [Route("auth/")]
    [ApiController]
    public class AuthController : Controller
    {
        public readonly IAuthRepository _repository;
        public readonly IConfiguration _config;
        public readonly ITokenService _tokenService;
        public AuthController(IAuthRepository repository, IConfiguration configuration, ITokenService tokenService )
        {
            _repository = repository;
            _config = configuration;
            _tokenService = tokenService;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignupModel signupModel)
        {
            
            //find our repository from unit of work
            var response = await _repository.Signup(signupModel);

         
            return Json(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult>  Login([FromBody] LoginModel loginModel)
        {
            var user = await _repository.Login(loginModel);
            if (user != null)
            {
                var response = new AuthResponse();
                var token = _tokenService.GenerateToken(_config["JWT:Key"], _config["JWT:Issuer"],user);
                response.Token = token;
                response.StatusCode = StatusCodes.Status200OK;
                response.Message = "Successfully loged in";
                return Json(response);
            }

            return NotFound();
        }
    }
}