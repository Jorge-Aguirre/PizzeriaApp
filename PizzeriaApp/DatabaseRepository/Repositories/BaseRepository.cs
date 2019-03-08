using DatabaseRepository.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseRepository.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly PizzeriaDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(PizzeriaDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public ICollection<T> GetAll()
        {
            IQueryable<T> query = _dbSet;
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Save(T entity)
        {
            _dbSet.Add(entity);

            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();
        }
    }
}
