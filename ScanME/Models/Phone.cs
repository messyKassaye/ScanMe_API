using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string phone_number { get; set; }
        public Company Company { get; set; }
    }
}
