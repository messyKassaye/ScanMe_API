using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Models
{
    public class AuthResponse
    {
        public int StatusCode { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public bool IsRegistered { get; set; }
    }
}
