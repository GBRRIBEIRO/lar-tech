using lar_tech.Domain.Models;

namespace lar_tech.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<List<T>> GetAllAsync();
        public Task<PaginatedResponse<T>> GetPaginatedAsync(IPaginatedRequest paginatedRequest);
        public Task<T?> GetByIdAsync(string id);
        public Task DeleteAsync(T entity);
        public Task DeleteByIdAsync(string id);
        public Task PostAsync(T entity);
        public Task PutAsync(T entity);
        public Task SaveAsync();
    }
}
