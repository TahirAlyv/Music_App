using Microsoft.EntityFrameworkCore;
using MusicService.Data;
using MusicService.Repository.Interface;
using System.Linq.Expressions;

namespace MusicService.Repository.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            var entry = await _context.Set<T>().AddAsync(entity);
            return entry.Entity;
        }

        public async Task  DeleteAsync(T entity)
        {
             _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludeAsync<TProperty>(Expression<Func<T, TProperty>> includeExpression)
        {
            return await _context.Set<T>()
                     .Include(includeExpression)
                     .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludeByConditionAsync<TProperty>(Expression<Func<T, bool>> predicate, Expression<Func<T, TProperty>> includeExpression)
        {
                return await _context.Set<T>()
                    .Where(predicate)
                    .Include(includeExpression)
                    .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result =await _context.Set<T>().FindAsync(id);
            return result!;
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public Task UpdateAsync(T entity)
        {
             _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }
    }
}
