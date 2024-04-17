using CursAPI.Enities;
using Microsoft.AspNetCore.Identity;

namespace CursAPI.Services.Identity
{
    public interface ITokenService
    {
        string CreateToken(User user, List<IdentityRole<Guid>> role);
    }
}
