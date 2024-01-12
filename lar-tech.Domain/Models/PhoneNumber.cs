using lar_tech.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lar_tech.Domain.Models
{
    public class PhoneNumber
    {
        public PhoneNumber(string personId, string number)
        {
            Id = Guid.NewGuid().ToString();
            PersonId = personId;
            Number = number;
        }

        public string Id { get; set; }
        public string PersonId { get; set; }
        public string Number { get; set; }
    }
}
