using lar_tech.Domain.Interfaces;
using lar_tech.Domain.Models;

namespace lar_tech.Data.Handlers
{
    public static class PaginationHandler
    {
        public async static Task<PaginatedResponse<T>> GetPaginated<T>(this PaginatedResponse<T> response, IEnumerable<T> list, IPaginatedRequest paginatedRequest) where T : class
        {
            //Checks and solves the null properties
            if (paginatedRequest.PageSize <= 0) paginatedRequest.PageSize = 10;
            if (paginatedRequest.Page <= 0) paginatedRequest.Page = 1;

            //Fill the TotalEntries property
            response.TotalEntries = list.Count();

            //Fill the Data property with the list
            response.Data = list.Skip((paginatedRequest.Page - 1) * paginatedRequest.PageSize)
                .Take(paginatedRequest.PageSize)
                .ToList();

            //Orders the Data property
            if (!string.IsNullOrEmpty(paginatedRequest.OrderBy)) response.Data = response.Data.OrderList(paginatedRequest.OrderBy, paginatedRequest.Order).ToList();

            //Fill the TotalPages property
            response.TotalPages = (int)Math.Abs((double)response.TotalEntries / paginatedRequest.PageSize);
            if (response.TotalPages == 0) response.TotalPages++;
            //Fill the Page property
            response.Page = paginatedRequest.Page;

            return response;
        }
    }
}
