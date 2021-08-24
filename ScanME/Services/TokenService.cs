using Microsoft.IdentityModel.Tokens;
using ScanME.DTO;
using ScanME.Models;
using ScanME.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ScanME.Services
{
    public class TokenService : ITokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 30;

        public string GenerateToken(string key, string issuer, Users users)
        {
            var claims = new[]
            {
                 new Claim(ClaimTypes.Name,users.Email),
                 new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
           expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        }

        public bool IsValidToken(string key, string issuer, string token)
        {
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
