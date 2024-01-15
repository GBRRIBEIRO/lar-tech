using lar_tech.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace lar_tech.Domain.ViewModels
{
    public class PhoneViewModel
    {
        public PhoneType PhoneType { get; set; }

        [MaxLength(12)]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public bool IsValid => long.TryParse(PhoneNumber, out var number);
    }
}
