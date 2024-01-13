using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace lar_tech.Domain.Models
{
    public class TokenResponse
    {
        public bool IsSuccessful => Errors.Count == 0;
        private List<string> Errors => new List<string>();
        public string Token { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? ExpirationDate { get; set; }

        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
