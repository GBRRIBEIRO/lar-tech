using lar_tech.Domain.Enums;
using lar_tech.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lar_tech.Domain.Filters
{
    public class PaginatedPersonRequest : IPaginatedRequest
    {
        public string? Name { get; set; }
        public DateOnly? Date { get; set; }
        public bool? IsActive { get; set; }
        public string? PhoneNumber { get; set; }

        //From Interface
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? OrderBy { get; set; }
        public OrderBy? Order { get; set; }

        public PersonFilterRequest ToFilterRequest()
        {
            return new PersonFilterRequest
            {
                Name = this.Name,
                Date = this.Date,
                IsActive = this.IsActive,
                PhoneNumber = this.PhoneNumber,
                Order = this.Order,
                OrderBy = this.OrderBy,
            };
        }
    }
}
