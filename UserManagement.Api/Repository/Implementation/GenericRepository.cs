using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Repository.Interface;

namespace UserManagement.Api.Repository.Implementation
{
    public class GenericRepository<T> :  IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _db;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        /// <summary>
        /// Deletes an Object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string id)
        {
            _db.Remove(await _db.FindAsync(id));
        }
        /// <summary>
        /// Deletes List of objects
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRangeAsync(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }
        /// <summary>
        /// Inserts An Object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(T entity)
        {
            try
            {
                await _db.AddAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(T item)
        {
            // attaches instance to the contex
            // t, then sets the state
            // as modified
            _db.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<IEnumerable<T>>GetAllAsync()
        {
           return _db;
        }

        public async Task<IQueryable<T>>GetAll(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            if (includes != null)
            {
                foreach (var property in includes)
                {
                    query = query.Include(property);
                }
            }
            return query.Where(expression);
        }
        
        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            if (includes != null)
            {
                foreach (var property in includes)
                {
                    query = query.Include(property);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }
    }
}