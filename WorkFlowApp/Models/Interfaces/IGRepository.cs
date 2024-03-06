using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowApp.Models.Interfaces
{
    public interface IGRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(Object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(Object id);
     
    }
}
