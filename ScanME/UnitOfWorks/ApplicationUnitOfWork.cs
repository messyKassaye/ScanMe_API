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
    public class ApplicationUnitOfWork<T> where T:class
    {
        public ApplicationDbContext _context;
        private GenericRepository<T> userRepository;

        public ApplicationUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public GenericRepository<T> ModelRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<T>(_context);
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
