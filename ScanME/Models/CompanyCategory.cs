using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Models
{
    public class CompanyCategory
    {
        public int CompanyCategoryId { get; set; }
        public string Name { get; set; }
        public List<Company> Companies { get; set; }
    }
}
