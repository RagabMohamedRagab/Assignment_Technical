using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Task.Core.Entities;
using Task.DataAccess.IRepositories;

namespace Task.DataAccess.Repositories
{
    public class AuthRepository(UserManager<user> userManager, SignInManager<user> signInManager, IHttpContextAccessor httpContextAccessor) : IAuthRepository
    {
        private readonly UserManager<user> _userManager = userManager;
        private readonly SignInManager<user> _signInManager = signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<bool> CreateAsync(user user, string Password)
        {
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("UserId", user.Id.ToString());
                return true;
            }
            return false;
        }
        public async Task<bool> AutenticatedUser(string Email, string Password)
        {
            var result = await _signInManager.PasswordSignInAsync(Email, Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var GetUser = await _userManager.FindByEmailAsync(Email);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("UserId", GetUser.Id.ToString());
                return true;
            }
            return false;
        }
        public async Task<bool> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
