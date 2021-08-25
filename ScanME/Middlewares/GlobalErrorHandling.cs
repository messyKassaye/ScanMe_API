using Microsoft.AspNetCore.Http;
using ScanME.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScanME.Middlewares
{
    public class GlobalErrorHandling
    {
        private RequestDelegate _next;

        public GlobalErrorHandling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }catch(Exception error)
            {
                var response = context.Response;
                response.ContentType = "Application/json";

                switch (error)
                {
                    case ConflictExceptions:
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    case NotFoundExceptions:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                }

                var errorResponse = new
                {
                    Message = error.Message,
                    StatusCode=response.StatusCode
                 };

                var jsonResponse = JsonSerializer.Serialize(errorResponse);
                await response.WriteAsync(jsonResponse);
               

            }
        }

    }
}
