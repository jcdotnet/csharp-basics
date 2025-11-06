using Microsoft.EntityFrameworkCore;
using VillaBookingApp.Application.RepositoryContracts;
using VillaBookingApp.Infrastructure.Data;
using System.Linq.Expressions;

namespace VillaBookingApp.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _db.SaveChangesAsync();
            //if (!tracked) _db.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return _dbSet.AnyAsync(filter);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, 
            string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query = PrepareQuery(filter, includeProperties, tracked);

            return await query.ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, 
            string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query = PrepareQuery(filter, includeProperties, tracked);

            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        private IQueryable<T> PrepareQuery(Expression<Func<T, bool>>? filter,
            string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = _dbSet;
            }
            else
            {
                query = _dbSet.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // include navigation properties
            if (!string.IsNullOrEmpty(includeProperties))
            {
                // NavigationProperty1, NavigationProperty2
                foreach (var includeProp in includeProperties
                    .Split([','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }
            return query;

        }
    }
}
