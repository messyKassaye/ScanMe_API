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
            var response = await _repository.Signup(signupModel);
            if (response.IsRegistered)
            {
                return await Login(new LoginModel() { Email = signupModel.Email, Password = signupModel.Password });
            }

            throw new ConflictExceptions(response.Message);
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

            throw new NotFoundExceptions("Unknown user. No one is registered by this email");
        }
    }
}
