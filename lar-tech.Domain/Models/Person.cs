using lar_tech.Domain.Interfaces;
using lar_tech.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lar_tech.Domain.Models
{
    public class Person
    {
        public Person() { }
        public Person(string name, DateTime birthDate, bool isActive, List<PhoneNumber> phoneNumbers)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            BirthDate = birthDate;
            IsActive = isActive;
            PhoneNumbers = phoneNumbers;
        }

        public Person(PersonViewModel viewModel)
        {
            Id = Guid.NewGuid().ToString();
            Name = viewModel.Name;
            BirthDate = viewModel.BirthDate.ToDateTime(TimeOnly.MinValue);
            IsActive = viewModel.IsActive;
            PhoneNumbers = viewModel.PhoneNumbers.Select(number => new PhoneNumber(this.Id, number)).ToList();
        }


        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "";
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

        public bool VerifyPhoneNumbers()
        {
            foreach (var phoneNumber in PhoneNumbers)
            {
                foreach (char c in phoneNumber.Number)
                {
                    if (c < '0' || c > '9')
                        return false;
                }
            }
            return true;
        }
    }
}
