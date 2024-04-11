using CursAPI.Enities;
using CursAPI.Models;
using CursAPI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CursAPI.Controllers
{
    /// <summary>
    /// Контроллер для пользователя
    /// </summary>
    public class UserController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        public UserController(ILogger logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("allusers")]
        public async Task<IActionResult> GetAllUser()
        {
            _logger.LogInformation("Получение всех пользователей.");

            return Ok(await _userService.GetAllUsers());
        }

        [HttpPost("addusers")]
        public async Task<IActionResult> AddUsers()
        {
            _logger.LogInformation("Добавление 1000 пользователей");

            return Ok(await _userService.AddUsers());
        }

        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser([FromBody] UserModel user)
        {
            _logger.LogInformation("Добавление пользователя");

            return Ok(await _userService.AddUser(user));
        }
    }
}
