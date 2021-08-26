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
        public GlobalAuthenticationHandler(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
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
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["JWT:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                // attach user to context on successful jwt validation
                context.Items["UserId"] = userId;
                await _next.Invoke(context);

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
