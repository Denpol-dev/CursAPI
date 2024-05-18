using CursAPI.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CursAPI.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger _logger;

        public BookController(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Получение всех книг
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(Routes.AllBooks)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [Authorize]
        public async Task<IActionResult> GetBook()
        {
            _logger.LogInformation("Работает");
            return Ok("Работает");
        }
    }
}
