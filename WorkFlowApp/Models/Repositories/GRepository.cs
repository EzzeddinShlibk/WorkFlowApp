
using Microsoft.EntityFrameworkCore;
using WorkFlowApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowApp.Models.Repositories
{
    public class GRepository<T> : IGRepository<T> where T : class
    {
        private readonly DataContext _context;
        private DbSet<T> table = null;
        public GRepository(DataContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public async Task<T> GetByIdAsync(Object id)
        {
            //var t= table.Find(id); 
            //return t; 
            return await table.FindAsync(id); // دالة في الانتتي للبحث من خلال الكي
        }
        public void Delete(Object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);

        }
        public IQueryable<T> GetAll()
        {
            return table.AsQueryable().AsNoTracking();
            //return table.AsQueryable();
        }
        public void Insert(T entity)
        {
            table.Add(entity);
        }
        public void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
  





    }
}
