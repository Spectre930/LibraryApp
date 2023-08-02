using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string[] includes = null);
        Task<IEnumerable<T>> GetAllAsync(string[] includes = null);
        Task AddAsync(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        int SetAge(DateTime dob);
    }
}
