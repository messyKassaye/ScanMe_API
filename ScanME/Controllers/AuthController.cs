using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ScanME.Exceptions;
using ScanME.Models;
using ScanME.Repository.Interfaces;
using ScanME.Services.Interfaces;
using System;
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

            //sign up response 
            var SignupResponse = await _repository.Signup(signupModel);
            if (SignupResponse.IsRegistered)
            {
              string token =  _tokenService.GenerateToken(_config["JWT:Key"], _config["JWT:Issuer"], SignupResponse.Users);
                var response = new
                {
                    StatusCode=StatusCodes.Status200OK,
                    Message = $"Hey, {SignupResponse.Users.FullName} you are successfully become a member of ScanME. Thank you for choosing us",
                    Token = token
                };
                return Json(response);
            }

            throw new ConflictExceptions(SignupResponse.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult>  Login([FromBody] LoginModel loginModel)
        {
            var user = await _repository.Login(loginModel);
            if (user != null)
            {
                
                var token = _tokenService.GenerateToken(_config["JWT:Key"], _config["JWT:Issuer"],user);
                var response = new
                {
                    Token = token,
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Successfully login"
                };
                return Json(response);
            }

            throw new NotFoundExceptions("Unknown user. No one is registered by this email");
        }
    }
}
