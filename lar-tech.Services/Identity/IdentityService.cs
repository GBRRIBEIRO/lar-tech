using lar_tech.Domain.Identity;
using lar_tech.Domain.Models;
using lar_tech.Domain.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace lar_tech.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtOptions _jwtOpt;

        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtOptions> jwtOpt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtOpt = jwtOpt.Value;
        }

        private string _GenerateToken(IEnumerable<Claim> userClaims)
        {
            //Creates the token using the data informed
            var token = new JwtSecurityToken
                (
                    issuer: _jwtOpt.Issuer,
                    audience: _jwtOpt.Audience,
                    claims: userClaims,
                    expires: DateTime.Now.AddSeconds(_jwtOpt.Expiration),
                    notBefore: DateTime.Now,
                    signingCredentials: _jwtOpt.SigningCredentials
                );

            //Write the token as string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<IEnumerable<Claim>> _GetUserClaims(string email)
        {
            //Get user by email
            var user = await _userManager.FindByEmailAsync(email);

            //Get user roles
            IEnumerable<string> userRoles = await _userManager.GetRolesAsync(user!);

            //Apply it to claims
            List<Claim> claims = new List<Claim>();
            foreach (var role in userRoles)
            {
                claims.Add(new Claim("role", role));
            }

            return claims;
        }


        public async Task<TokenResponse> LoginUser(LoginViewModel loginRequest)
        {
            //Creates an empty response
            var response = new TokenResponse();

            var result = await _signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, false);

            if (!result.Succeeded)
            {
                response.AddError("Failed to login!"); return response;
            }

            //Get claims
            var claims = await _GetUserClaims(loginRequest.Email);
            //Assign generated token to the response
            response.Token = _GenerateToken(claims);
            response.ExpirationDate = DateTime.Now.AddSeconds(_jwtOpt.Expiration);

            return response;

        }

        public async Task<TokenResponse> RegisterUser(RegisterViewModel registerRequest)
        {
            //Creates an empty response
            var response = new TokenResponse();

            //Creates the user
            var user = new ApplicationUser
            {
                Email = registerRequest.Email,
                UserName = registerRequest.Email,
                EmailConfirmed = true,
            };
            var success = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!success.Succeeded) { response.AddError("Failed to create user!"); return response; }
            //Enables user
            await _userManager.SetLockoutEnabledAsync(user, false);

            //Generates token
            //Get claims
            var claims = await _GetUserClaims(registerRequest.Email);
            //Assign generated token to the response
            response.Token = _GenerateToken(claims);
            response.ExpirationDate = DateTime.Now.AddSeconds(_jwtOpt.Expiration);

            return response;
        }

        public async Task<bool> PromoteUserByEmail(string email)
        {
            try
            {
                //Get user
                var user = await _userManager.FindByEmailAsync(email);
                //Assign
                await _userManager.AddToRoleAsync(user, "Admin");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveAdminByEmail(string email)
        {
            try
            {
                //Get user
                var user = await _userManager.FindByEmailAsync(email);
                //Remove
                await _userManager.RemoveFromRoleAsync(user, "Admin");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
