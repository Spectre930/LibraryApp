using LibraryApp.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LibraryContext _db;
        internal DbSet<T> dbSet;

        public Repository(LibraryContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(string[] includes)
        {
            IQueryable<T> query = dbSet;

            if(includes != null) 
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string[] includes = null)
        {
            IQueryable<T> query = dbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
           
            return await query.FirstOrDefaultAsync(filter);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
