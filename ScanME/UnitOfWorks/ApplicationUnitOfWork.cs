using ScanME.Contexts;
using ScanME.Helpers;
using ScanME.Middlewares;
using ScanME.Models;
using ScanME.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.UnitOfWorks
{
    public class ApplicationUnitOfWork
    {
        public ApplicationDbContext _context;
        private GenericRepository<Users> userRepository;

        public ApplicationUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public GenericRepository<Users> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<Users>(_context);
                }

                return this.userRepository;
            }
        }

        public int Save()
        {
           return _context.SaveChanges();
        }
    }
}
