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

        [HttpGet("books")]
        public async Task<IActionResult> GetBook()
        {
            _logger.LogInformation("Работает");
            return Ok("Работает");
        }
    }
}
