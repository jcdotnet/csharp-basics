using System.Linq.Expressions;

namespace VillaBookingApp.Application.RepositoryContracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, 
            string? includeProperties = null, bool tracked = false);
        
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, 
            string? includeProperties = null, bool tracked = false);
        
        Task<T> AddAsync(T entity);
        
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        
        Task RemoveAsync(T entity);
    }
}
