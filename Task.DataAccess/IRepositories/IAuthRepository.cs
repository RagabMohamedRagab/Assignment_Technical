
using Task.Core.Entities;

namespace Task.DataAccess.IRepositories
{
    public interface  IAuthRepository
    {
        Task<bool> CreateAsync(user user,string Password);
        Task<bool> AutenticatedUser(string Email,string Password);

        Task<bool> LogOut();
    }
}
