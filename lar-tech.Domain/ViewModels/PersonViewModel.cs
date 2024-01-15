using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace lar_tech.Domain.ViewModels
{
    public class PersonViewModel
    {
        [MinLength(11)]
        [MaxLength(11)]
        [JsonPropertyName("cpf")]
        public string CpfStr { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public bool IsActive { get; set; }
        public List<PhoneViewModel> PhoneNumbers { get; set; } = new List<PhoneViewModel>();
    }
}
