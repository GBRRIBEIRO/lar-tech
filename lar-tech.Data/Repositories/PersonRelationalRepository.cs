using lar_tech.Data.Database;
using lar_tech.Domain.Enums;
using lar_tech.Domain.Filters;
using lar_tech.Services.Handlers;
using lar_tech.Domain.Interfaces;
using lar_tech.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace lar_tech.Data.Repositories
{
    public class PersonRelationalRepository<T> : Repository<T> where T : Person
    {
        public PersonRelationalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<T>> GetAllFiltered(PersonFilterRequest personFilter)
        {
            //Transforms the DbSet into a IQueriable
            var _query = _dbSet.Include(p => p.PhoneNumbers).AsQueryable();

            //Apply the filters
            _query = _query.Filter(personFilter);

            //Get the filtered results
            var result = await _query.ToListAsync();

            //Order the results if it is declared in the query
            if (!string.IsNullOrEmpty(personFilter.OrderBy))
            {
                var orderedResult = result.AsEnumerable().OrderList(personFilter.OrderBy, personFilter.Order);
                result = orderedResult.ToList();
            }

            return result;
        }
        public async Task<PaginatedResponse<T>> GetPaginatedAndFiltered(PaginatedPersonRequest paginatedRequest)
        {

            //Creates the empty response
            var response = new PaginatedResponse<T>();

            //Creates the IQueryable
            var query = _dbSet.Include(p => p.PhoneNumbers).AsQueryable();
            //Transform into a non paginated request to use the filters
            query = query.Filter(paginatedRequest.ToFilterRequest());
            //Gets the results of the query
            var result = await query.ToListAsync();

            //Returns the response paginated
            return await response.GetPaginated(result.AsEnumerable(), paginatedRequest);
        }

        public override async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.Include(p => p.PhoneNumbers).ToListAsync();
        }

        public override async Task<T?> GetByIdAsync(string id)
        {
            return await _dbSet.Include(p => p.PhoneNumbers).Where(p => p.Id == id).FirstOrDefaultAsync();
        }

    }
}
