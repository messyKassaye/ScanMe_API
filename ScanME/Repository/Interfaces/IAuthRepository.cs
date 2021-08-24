using ScanME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Repository.Interfaces
{
   public interface IAuthRepository
    {
       Task<Users> Login(LoginModel loginModel);
       Task<Users> Signup(SignupModel signupModel);
       Task<Users> GetUser(string email);
       Task<bool> CheckUserExistence(string email);
    }
}
