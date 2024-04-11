using CursAPI.Enities;

namespace CursAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> AddUsers(User user);

        Task<List<User>> GetAllUsers();
    }
}
