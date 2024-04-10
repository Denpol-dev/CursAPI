using CursAPI.Enities;

namespace CursAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> AddUsers();

        Task<List<User>> GetAllUsers();
    }
}
