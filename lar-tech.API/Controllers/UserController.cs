using lar_tech.Domain.Identity;
using lar_tech.Domain.Interfaces;
using lar_tech.Domain.Models;
using lar_tech.Domain.ViewModels;
using lar_tech.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lar_tech.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<ApplicationUser> _userManager;


        public UserController(IIdentityService identityService, IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _userManager = unitOfWork.UserRepository;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginViewModel loginVM)
        {
            var result = await _identityService.LoginUser(loginVM);
            if (!result.IsSuccessful) return BadRequest();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<UserRoles>>> GetAllUsers()
        {
            var result = await _userManager.Users.ToListAsync();
            if (result.Count == 0) return NoContent();

            var userRoles = new List<UserRoles>();
            foreach (var user in result)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles.Add(new UserRoles { ApplicationUser = user, Roles = roles.ToList() });
            }

            return Ok(userRoles);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{email}")]
        public async Task<ActionResult> DeleteUserByEmail([FromRoute] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NoContent();
            await _userManager.DeleteAsync(user);
            return Ok();
        }

        [HttpPost("register")]
        public async Task<ActionResult<TokenResponse>> Register([FromBody] RegisterViewModel registerVM)
        {
            var result = await _identityService.RegisterUser(registerVM);
            if (!result.IsSuccessful) return BadRequest();
            return Ok(result);
        }

        //Only admin can promote other user to admin
        [Authorize(Roles = "Admin")]
        [HttpPost("promote/{email}")]
        public async Task<ActionResult> PromoteToAdmin([FromRoute] string email)
        {
            //Promote user
            var success = await _identityService.PromoteUserByEmail(email);
            if (!success) return BadRequest();
            return Ok();
        }

        //Only admin can remove an admin
        [Authorize(Roles = "Admin")]
        [HttpPost("removeadmin/{email}")]
        public async Task<ActionResult> RemoveAdmin([FromRoute] string email)
        {
            //Promote user
            var success = await _identityService.RemoveAdminByEmail(email);
            if (!success) return BadRequest();
            return Ok();
        }

    }
}
