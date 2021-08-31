using ScanME.DTO;
using ScanME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Services.Interfaces
{
    public interface IUserService
    {
        UserDTO Me(int userId);
    }
}
