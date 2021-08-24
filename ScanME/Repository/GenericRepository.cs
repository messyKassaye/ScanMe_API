

using Microsoft.EntityFrameworkCore;
using ScanME.Contexts;
using ScanME.Models;
using ScanME.Repository.Interfaces;
using System.Collections.Generic;

namespace ScanME.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public ApplicationDbContext _context;
        public DbSet<T> _dbset;

        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;
            _dbset = _context.Set<T>();
        }
        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return  _dbset;
        }

        public T Show(int id)
        {
            return _dbset.Find(id);
        }

        public void Store(T entity)
        {
            _dbset.Add(entity);
        }

        public void Update(int id, T entity)
        {
            entity = Show(id);
            _dbset.Update(entity);
        }
    }
}
