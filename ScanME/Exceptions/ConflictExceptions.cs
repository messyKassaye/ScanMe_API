using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Exceptions
{
    public class ConflictExceptions:Exception
    {
        public ConflictExceptions(string message) : base(message)
        {

        }
    }
}
