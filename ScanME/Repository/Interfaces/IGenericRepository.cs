using ScanME.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Repository.Interfaces
{
    interface IGenericRepository<T> where T:class
    {

        public  IEnumerable<T> GetAll();
        public T Show(int id);
        public void Store(T entity);
        public void Update(int id, T entity);
        public void Delete(int id);
    }
}
