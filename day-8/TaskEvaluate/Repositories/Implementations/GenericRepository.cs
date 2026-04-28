using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TaskEvaluate.Data.Contexts;
using TaskEvaluate.Repositories.Interfaces;

namespace TaskEvaluate.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly Test06DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(Test06DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}
