using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Middlewares
{
    public class GlobalAuthenticationHandler
    {
        private readonly RequestDelegate _next;

        public async Task Invoke(HttpContext context)
        {
            try
            {

            }catch(Exception ex)
            {

            }
        }
    }
}
