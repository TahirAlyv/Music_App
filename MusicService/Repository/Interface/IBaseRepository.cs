using MusicService.Dtos;
using System.Linq.Expressions;

namespace MusicService.Repository.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> SaveChangesAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllWithIncludeAsync<TProperty>(Expression<Func<T, TProperty>> includeExpression);
        Task<IEnumerable<T>> GetAllWithIncludeByConditionAsync<TProperty>(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, TProperty>> includeExpression);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<List<PlaylistAndItemDto>> GetPlaylistsWithItemsAndMusicAsync(string userId);
        Task<List<T>> GetManyAsync(Expression<Func<T, bool>> predicate);
    }

}

