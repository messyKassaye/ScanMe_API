using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ScanME.Contexts;
using ScanME.Helpers;
using ScanME.Models;
using ScanME.Repository.Interfaces;
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

        public async Task<Users> CheckUsersEmailAndPhon(string email,string phone)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email||u.Phone == phone);
        }

        public async Task<Users> GetUsers(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Users> Login(LoginModel loginModel)
        {
            var user = await GetUsers(loginModel.Email);
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


        public async Task<AuthResponse> Signup(SignupModel signupModel)
        {
            //Check if some one registered by this email address
            var CheckUserExistency = await CheckUsersEmailAndPhon(signupModel.Email,signupModel.Phone);
            if(CheckUserExistency != null)
            {
                return new AuthResponse()
                {
                    Message = "Some one is registered by your email or phone. Please try again",
                    StatusCode = StatusCodes.Status409Conflict,
                    Token = null,
                    IsRegistered=false
                };
            }

            //register new user. If it is not existed
            var Users = new Users()
            {
                FullName = signupModel.FullName,
                Email = signupModel.Email,
                Phone = signupModel.Phone,
                RoleId=1,
                Password = EncryptionHandler.HashPassword(signupModel.Password),
            };

            await _context.Users.AddAsync(Users);
            int result = await _context.SaveChangesAsync();

            var response = new AuthResponse();

            if (result == 1)
            {
                response.Message = "Successfully registered";
                response.StatusCode = StatusCodes.Status200OK;
                response.Token = null;
                response.IsRegistered = true;
                response.Users = Users;
            }
            else
            {
                response.Message = "Not registered";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Token = null;
                response.IsRegistered = false;
            }

            return response;
        }
    }
}
