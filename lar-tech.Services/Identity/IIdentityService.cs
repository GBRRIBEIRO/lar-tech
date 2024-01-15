using lar_tech.Domain.Models;
using lar_tech.Domain.ViewModels;

namespace lar_tech.Services.Identity
{
    public interface IIdentityService
    {
        public Task<TokenResponse> RegisterUser(RegisterViewModel registerRequest);
        public Task<TokenResponse> LoginUser(LoginViewModel loginRequest);
        public Task<bool> PromoteUserByEmail(string email);
        public Task<bool> RemoveAdminByEmail(string email);
    }
}