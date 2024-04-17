using Microsoft.AspNetCore.Mvc;

namespace CursAPI.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger _logger;

        public BookController(ILogger logger)
        {
            _logger = logger;
        }

        //[HttpGet("allusers")]
        //public async Task<IActionResult> GetAllUser()
        //{
        //    _logger.LogInformation("Получение всех пользователей.");

        //    return Ok(await _userService.GetAllUsers());
        //}
    }
}
