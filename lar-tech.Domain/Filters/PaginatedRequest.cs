using lar_tech.Domain.Enums;
using lar_tech.Domain.Interfaces;

namespace lar_tech.Domain.Filters
{
    public class PaginatedRequest : IPaginatedRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? OrderBy { get; set; }
        public OrderBy? Order { get; set; }
    }

}
