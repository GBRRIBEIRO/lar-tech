using lar_tech.Domain.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace lar_tech.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuthenticationCustom(this IServiceCollection services, IConfiguration configuration)
        {
            //This variable holds the information from the JwtOptions from appsettings.json
            var jwtOptionsSettings = configuration.GetSection("JwtOptions");
            //Creates the encrypted security key
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtOptions:SecurityKey").Value));

            //Configures an option class, in this case the JwtOption
            services.Configure<JwtOptions>(jwtOptions =>
            {
                jwtOptions.Issuer = jwtOptionsSettings["Issuer"];
                jwtOptions.Audience = jwtOptionsSettings["Audience"];
                jwtOptions.SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                jwtOptions.Expiration = int.Parse(jwtOptionsSettings["Expiration"]);
            });

            //Configures an option class, in this case the IdentityOptions
            //Sets the password min lenght
            services.Configure<IdentityOptions>(identityOptions =>
            {
                identityOptions.Password.RequiredLength = 4;
            });

            //Creates the token validation parameter
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptionsSettings["Issuer"],

                ValidateAudience = true,
                ValidAudience = jwtOptionsSettings["Audience"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt => opt.TokenValidationParameters = tokenValidationParameters);
        }


    }
}
