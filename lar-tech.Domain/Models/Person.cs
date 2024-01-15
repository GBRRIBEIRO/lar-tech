using lar_tech.Domain.ViewModels;

namespace lar_tech.Domain.Models
{
    public class Person
    {
        public Person() { }
        public Person(long cpf, string name, DateTime birthDate, bool isActive, List<PhoneNumber> phoneNumbers)
        {
            Cpf = cpf;
            Name = name;
            BirthDate = birthDate;
            IsActive = isActive;
            PhoneNumbers = phoneNumbers;
        }

        public Person(PersonViewModel viewModel)
        {
            Cpf = long.Parse(viewModel.CpfStr);
            Name = viewModel.Name;
            BirthDate = viewModel.BirthDate.ToDateTime(TimeOnly.MinValue);
            IsActive = viewModel.IsActive;
            PhoneNumbers = viewModel.PhoneNumbers.Select(number => new PhoneNumber(this.Id, number.PhoneType, long.Parse(number.PhoneNumber))).ToList();
        }


        public string Id { get; set; } = Guid.NewGuid().ToString();
        public long Cpf { get; set; }
        public string Name { get; set; } = "";
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

        public static Person? ValidadeAndCreateObject(PersonViewModel personVM)
        {
            bool success = long.TryParse(personVM.CpfStr, out var cpf);

            if (!success) return null;

            return new Person(personVM);
        }
    }
}
