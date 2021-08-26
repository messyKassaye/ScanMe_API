using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScanME.Middlewares
{
    public class GlobalAuthenticationHandler
    {
        private readonly RequestDelegate _next;

        public GlobalAuthenticationHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string currentRoute = context.Request.Path;
            if (currentRoute.Equals("/auth/signup") || currentRoute.Equals("/auth/login"))
            {
                await _next.Invoke(context);
            }

            string Authorization = context.Request.Headers["Authorization"];
            var response = context.Response;
            if (Authorization != null)
            {

                string Token = Authorization.Split(new char[] { ' ' })[1];
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
