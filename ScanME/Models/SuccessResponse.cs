using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScanME.Models
{
    public class SuccessResponse
    {
        public int StatusCode { get; set; }
        public string Status { get; set; }
        [DefaultValue(1)]
        public int StatusResult { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
