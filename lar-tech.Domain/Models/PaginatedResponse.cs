using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lar_tech.Domain.Interfaces;

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
