using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScanME.Contexts;
using ScanME.DTO;
using ScanME.Models;
using ScanME.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Services
{
    public class UserService : IUserService
    {
        public ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public UserDTO Me(int userId)
        {
            IQueryable<Users> users = _context.Users.Where(u=>u.UsersId==userId).Include(u=>u.Role).Include(u => u.Company)
                .ThenInclude(c=>c.Category);
            var userDTO = _mapper.Map<UserDTO>(users.First());
            return userDTO;
        }
    }
}
