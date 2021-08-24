using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public List<Users> Users { get; set; }
    }
}
