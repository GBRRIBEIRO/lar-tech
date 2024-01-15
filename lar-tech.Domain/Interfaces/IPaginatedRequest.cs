namespace lar_tech.Domain.Interfaces
{
    public interface IPaginatedRequest : IFilter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
