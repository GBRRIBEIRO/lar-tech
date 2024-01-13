using lar_tech.Services.Handlers;
using lar_tech.Data.Database;
using lar_tech.Domain.Interfaces;
using lar_tech.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace lar_tech.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _dbContext;
        protected DbSet<T> _dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<PaginatedResponse<T>> GetPaginatedAsync(IPaginatedRequest paginatedRequest)
        {
            //Creates the empty response
            var response = new PaginatedResponse<T>();

            //Creates the IQueryable
            var query = _dbSet.AsQueryable();

            //Gets the results of the query
            var result = await query.ToListAsync();

            //Returns the response paginated
            return await response.GetPaginated(result.AsEnumerable(), paginatedRequest);
        }

        public virtual async Task PostAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public virtual async Task PutAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveAsync();
        }
        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public virtual async Task DeleteByIdAsync(string id)
        {
            var result = await GetByIdAsync(id);
            if (result != null) await DeleteAsync(result);
        }


        public virtual async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
