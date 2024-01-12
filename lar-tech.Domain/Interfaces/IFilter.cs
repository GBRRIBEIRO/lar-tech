using lar_tech.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lar_tech.Domain.Interfaces
{
    public interface IFilter
    {
        public string? OrderBy { get; set; }
        public OrderBy? Order { get; set; }
    }
}
