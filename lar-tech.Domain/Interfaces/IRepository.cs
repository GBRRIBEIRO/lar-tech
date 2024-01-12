using lar_tech.Domain.Filters;
using lar_tech.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
