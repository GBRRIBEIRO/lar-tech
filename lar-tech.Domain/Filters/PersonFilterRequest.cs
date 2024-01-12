using lar_tech.Domain.Enums;
using lar_tech.Domain.Interfaces;

namespace lar_tech.Domain.Filters
{
    public class PersonFilterRequest : IFilter
    {
        public string? Name { get; set; }
        public DateOnly? Date { get; set; }
        public bool? IsActive { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OrderBy { get; set; }
        public OrderBy? Order { get; set; }
    }
}
