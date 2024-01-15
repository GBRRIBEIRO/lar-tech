namespace lar_tech.Domain.Models
{
    public class PaginatedResponse<T> where T : class
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalEntries { get; set; }
        public List<T> Data { get; set; } = new List<T>();
    }
}
