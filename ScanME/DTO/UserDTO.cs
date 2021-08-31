using ScanME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.DTO
{
    public class UserDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public RoleDTO Role { get; set; }
        public CompanyDTO Company { get; set; }
    }
}
