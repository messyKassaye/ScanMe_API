using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanME.Models
{
    public abstract class AppResponse<T> where T:class
    {
        public abstract T Success(T entity);
        public abstract T Fail(T entity);
        public abstract T Error(T entity);
    }
}
