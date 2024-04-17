using CursAPI.Enities;
using CursAPI.Models;

namespace CursAPI.Services.UserServices
{
    public interface IUserService
    {
        Task<User?> GetUserByEmail(string email);
        Task<AuthResponse> Authenticate(User user);
        Task<object?> Registration(RegisterRequest request);
    }
}
