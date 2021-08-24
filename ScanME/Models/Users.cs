using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Models
{
    public class Users
    {

        public int UsersId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public List<Role> Role { get; set; }
    }
}
