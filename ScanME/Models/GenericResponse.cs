using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Models
{
    public class GenericResponse<T> where T:class
    {

        public T entity;
        public GenericResponse(T entity) {
            this.entity = entity;
        }
        public T GetResponse()
        {
            return entity;
        }
    }
}
