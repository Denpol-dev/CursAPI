using CursAPI.Enities;
using CursAPI.Extensions;
using CursAPI.Models;
using CursAPI.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CursAPI.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        private readonly ITokenService _token;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public UserService(Context context, ITokenService token, IConfiguration configuration, UserManager<User> userManager)
        {
            _context = context;
            _token = token;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<AuthResponse> Authenticate(User user)
        {
            var roleIds = await _context.UserRoles.Where(r => r.UserId == user.Id).Select(x => x.RoleId).ToListAsync();
            var roles = await _context.Roles.Where(x => roleIds.Contains(x.Id)).ToListAsync();

            var accessToken = _token.CreateToken(user, roles);
            user.RefreshToken = _configuration.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());

            await _context.SaveChangesAsync();

            return new AuthResponse
            {
                Username = user.UserName!,
                Email = user.Email!,
                Token = accessToken,
                RefreshToken = user.RefreshToken
            };
        }

        public async Task<object?> Registration(RegisterRequest request)
        {
            var user = new User
            {
                Name = request.FirstName + " " + request.LastName + " " + request.MiddleName,
                Email = request.Email,
                UserName = request.Email
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) return null;

            var findUser = 
                await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email) 
                ?? 
                throw new Exception($"User {request.Email} not found");

            await _userManager.AddToRoleAsync(findUser, RoleConstants.Member);

            return new AuthenticationRequest
            {
                Email = request.Email,
                Password = request.Password
            };
        }
    }
}
