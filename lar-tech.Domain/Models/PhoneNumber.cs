using lar_tech.Domain.Enums;

namespace lar_tech.Domain.Models
{
    public class PhoneNumber
    {
        public PhoneNumber()
        {

        }
        public PhoneNumber(string personId, PhoneType type, long number)
        {
            Id = Guid.NewGuid().ToString();
            PersonId = personId;
            PhoneType = type;
            Number = number;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PersonId { get; set; }
        public PhoneType PhoneType { get; set; }
        public long Number { get; set; }

    }
}
