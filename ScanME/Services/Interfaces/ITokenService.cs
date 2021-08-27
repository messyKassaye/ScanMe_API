using ScanME.DTO;
using ScanME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Services.Interfaces
{
    public interface ITokenService
    {
       string GenerateToken(string key,string issuer,Users userDTO);
       bool IsValidToken(string key, string issuer, string token);

        int FindSubject(string key,string token);
    }
}
