﻿using CursAPI.Enities;
using CursAPI.Extensions;
using CursAPI.Models;
using CursAPI.Services.UserServices;
using CursAPI.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace CursAPI.Controllers
{
    /// <summary>
    /// Контроллер для пользователя
    /// </summary>
    public class UserController : Controller
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public UserController(ILogger logger, IUserService userService, UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Вход и получение токена через Email
        /// </summary>
        /// <param name="authenticationRequest"></param>
        /// <remarks>
        /// Для аутентификации необходимо ввести Email и пароль
        /// </remarks>
        /// <returns>Токен</returns>
        [HttpPost]
        [Route(Routes.LoginRoute)]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managedUser = await _userManager.FindByEmailAsync(authenticationRequest.Email);
            if (managedUser == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, authenticationRequest.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var user = await _userService.GetUserByEmail(authenticationRequest.Email);

            if (user is null)
                return Unauthorized("User does not exist");

            return Ok(await _userService.Authenticate(user));
        }

        /// <summary>
        /// Регистрация и получение токена
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns>Токен</returns>
        [HttpPost]
        [Route(Routes.RegistrationRoute)]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid) return BadRequest(registerRequest);

            var registrationRequest = await _userService.Registration(registerRequest);
            //if (!await _roleManager.RoleExistsAsync(RoleConstants.Member))
            //{
            //    await _roleManager.CreateAsync(new IdentityRole(RoleConstants.Member));
            //}
            if (registrationRequest is null)
                return BadRequest(registerRequest);
            else if (registrationRequest is AuthenticationRequest ar)
                return await Authenticate(ar);
            else if (registrationRequest is string)
                return BadRequest(registrationRequest);

            return BadRequest(registerRequest);
        }

        /// <summary>
        /// Получение нового токена
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns>Токен</returns>
        [HttpPost]
        [Route(Routes.RefreshRoute)]
        [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var accessToken = tokenModel.AccessToken;
            var refreshToken = tokenModel.RefreshToken;
            var principal = _configuration.GetPrincipalFromExpiredToken(accessToken);

            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var username = principal.Identity!.Name;
            var user = await _userManager.FindByNameAsync(username!);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = _configuration.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _configuration.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }
    }
}
