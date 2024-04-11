using CursAPI.Enities;
using CursAPI.Models;

namespace CursAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> AddUsers();
        Task<bool> AddUser(UserModel user);
        Task<List<User>> GetAllUsers();
    }
}
