using System.Linq.Expressions;
using AngularBlog.Core.Responses;

namespace AngularBlog.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<ApiResponse<T>> GetByIdAsync(int id);
        Task<ApiResponse<List<T>>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
