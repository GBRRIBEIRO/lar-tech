using Microsoft.IdentityModel.Tokens;

namespace lar_tech.Domain.Identity
{
    public class JwtOptions
    {
        //A model for the same data in appsettings.json
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public int Expiration { get; set; }
    }
}
