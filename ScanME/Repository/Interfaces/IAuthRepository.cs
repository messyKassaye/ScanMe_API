using Microsoft.AspNetCore.Mvc;
using ScanME.Models;
using System.Threading.Tasks;

namespace ScanME.Repository.Interfaces
{
   public interface IAuthRepository
    {
       Task<Users> Login(LoginModel loginModel);
       Task<AuthResponse> Signup(SignupModel signupModel);
       Task<Users> CheckUsersEmailAndPhon(string email,string phone);
       Task<Users> GetUsers(string email);
    }
}
