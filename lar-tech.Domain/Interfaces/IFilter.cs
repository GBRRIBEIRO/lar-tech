using lar_tech.Domain.Enums;

namespace lar_tech.Domain.Interfaces
{
    public interface IFilter
    {
        public string? OrderBy { get; set; }
        public OrderBy? Order { get; set; }
    }
}
