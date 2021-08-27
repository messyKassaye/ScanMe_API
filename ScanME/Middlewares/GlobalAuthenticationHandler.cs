using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ScanME.Contexts;
using ScanME.Repository.Interfaces;
using ScanME.Services.Interfaces;
using ScanME.UnitOfWorks;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScanME.Middlewares
{
    public class GlobalAuthenticationHandler
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        public GlobalAuthenticationHandler(RequestDelegate next, IConfiguration config,ITokenService tokenService)
        {
            _next = next;
            _config = config;
            _tokenService = tokenService;
        }

        public async Task Invoke(HttpContext context)
        {
            //gloabl routes
            string currentRoute = context.Request.Path;
            if (currentRoute.Equals("/auth/signup") || currentRoute.Equals("/auth/login"))
            {
                await _next.Invoke(context);
            }

            //check authorization from the incoming requests and check JWT expirations as well
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var response = context.Response;
            if (token != null)
            {
                var tokenValidation = _tokenService.IsValidToken(_config["JWT:Key"],_config["JWT:Issuer"],token);
                if (tokenValidation)
                {
                    var userId = _tokenService.FindSubject(_config["JWT:Key"], token);
                    // attach user to context on successful jwt validation
                    context.Items["UserId"] = userId;
                    await _next.Invoke(context);
                }
            }

            response.ContentType = "Application/json";
            response.StatusCode = (int)HttpStatusCode.Unauthorized;

            var jsonResponse = new
            {
                Message = "Authentication is required",
                StatusCode = response.StatusCode
            };
            await response.WriteAsync(JsonSerializer.Serialize(jsonResponse));

        }
    }
}
