using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UserManagement.Api.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
         Task DeleteAsync(string id);
        void DeleteRangeAsync(IEnumerable<T> entities);
        Task InsertAsync(T entity);
        void Update(T item);
        Task<IEnumerable<T>>GetAllAsync();
        Task<IQueryable<T>>GetAll(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string> includes = null);
    }
}