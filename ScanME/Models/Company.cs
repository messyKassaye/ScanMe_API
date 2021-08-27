using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public CompanyCategory Category { get; set; }
        public int UsersId { get; set; }
        public Users Users { get; set; }

        public List<Phone> Phone { get; set; }
    }
}
