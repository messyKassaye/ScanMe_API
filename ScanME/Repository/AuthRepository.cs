using Microsoft.EntityFrameworkCore;
using ScanME.Contexts;
using ScanME.Helpers;
using ScanME.Models;
using ScanME.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> CheckUserExistence(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<Users> GetUser(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Users> Login(LoginModel loginModel)
        {
            var user = await GetUser(loginModel.Email);
            if (user != null)
            {
                var verify = EncryptionHandler.VerifyPassword(loginModel.Password, user.Password);
                if (verify)
                {
                    return user;
                }
                return null;
            }
            return null;
        }

        public Task<Users> Signup(SignupModel signupModel)
        {
            throw new NotImplementedException();
        }
    }
}
