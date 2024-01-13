using lar_tech.Domain.Identity;
using lar_tech.Domain.Models;
using lar_tech.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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