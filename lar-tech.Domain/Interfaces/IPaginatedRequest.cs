using lar_tech.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lar_tech.Domain.Interfaces
{
    public interface IPaginatedRequest : IFilter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
