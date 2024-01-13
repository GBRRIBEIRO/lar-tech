using lar_tech.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lar_tech.Domain.ViewModels
{
    public class PersonViewModel
    {
        public string Name { get; set; } = "";
        public DateOnly BirthDate { get; set; }
        public bool IsActive { get; set; }
        public List<PhoneViewModel> PhoneNumbers { get; set; } = new List<PhoneViewModel>();
    }
}
