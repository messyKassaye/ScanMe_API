using ScanME.Contexts;
using ScanME.Repository;

namespace ScanME.UnitOfWorks
{
    public class ApplicationUnitOfWork<T> where T:class
    {
        public ApplicationDbContext _context;
        private GenericRepository<T> modelRepository;

        public ApplicationUnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public GenericRepository<T> ModelRepository
        {
            get
            {
                if (this.modelRepository == null)
                {
                    this.modelRepository = new GenericRepository<T>(_context);
                }

                return this.modelRepository;
            }
        }

        

        public int Save()
        {
           return _context.SaveChanges();
        }
    }
}
