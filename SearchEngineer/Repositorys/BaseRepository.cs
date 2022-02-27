using Microsoft.EntityFrameworkCore;
using SearchEngineer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SearchEngineer.Repositorys
{
    public class BaseRepository: IBaseRepository
    {
        protected AppDbContext _context { get; private set; }

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return _context.Set<T>().Where<T>(funcWhere);
        }

        public async Task<bool> Any<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return await _context.Set<T>().AnyAsync<T>(funcWhere);
        }

        public async Task<T> Insert<T>(T t) where T : class
        { 
            await _context.Set<T>().AddAsync(t);
            return t;
        }

        public bool Update<T>(T t) where T : class
        {
            if (t == null)
            {
                return false;
            }

            _context.Set<T>().Attach(t);
            _context.Entry<T>(t).State = EntityState.Modified;
            return true;
        }

        public bool Delete<T>(T t) where T : class
        {
            if (t == null)
            {
                return false;
            }

            _context.Set<T>().Remove(t);
            return true;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
