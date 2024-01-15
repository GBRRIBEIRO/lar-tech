using lar_tech.Domain.Filters;
using lar_tech.Domain.Models;

namespace lar_tech.Services.Handlers
{
    public static class PersonFilterHandler
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, PersonFilterRequest personFilter) where T : Person
        {
            //Filter using IQueryable
            if (personFilter.IsActive != null) query = query.Where(p => p.IsActive == personFilter.IsActive);

            if (!string.IsNullOrEmpty(personFilter.Name)) query = query.Where(p => p.Name == personFilter.Name);

            if (personFilter.Date != null) query = query.Where(p => p.BirthDate.Date == personFilter.Date.Value.ToDateTime(TimeOnly.MinValue));

            if (personFilter.PhoneNumber != null) query = query.Where(p => p.PhoneNumbers.Any(phone => phone.Number == personFilter.PhoneNumber));

            return query;
        }
    }
}
