using lar_tech.Domain.Enums;
using lar_tech.Domain.Interfaces;
using lar_tech.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lar_tech.Domain.Models
{
    public class PhoneNumber
    {
        public PhoneNumber()
        {

        }
        public PhoneNumber(string personId, PhoneType type, string number)
        {
            Id = Guid.NewGuid().ToString();
            PersonId = personId;
            PhoneType = type;
            Number = number;
        }

        public string Id { get; set; }
        public string PersonId { get; set; }
        public PhoneType PhoneType { get; set; }
        public string Number { get; set; }
    }
}
