using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Web.Controllers
{
    /// <summary>
    /// Контроллер для аутентификации пользователя: регистрация, вход и выход.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Инициализация контроллера аутентификации.
        /// </summary>
        /// <param name="authService">Сервис аутентификации пользователя.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <param name="newUser">Данные для регистрации пользователя.</param>
        /// <returns>Результат регистрации пользователя и автоматического входа.</returns>
        /// <response code="200">Успешная регистрация и вход.</response>
        /// <response code="404">Ошибка регистрации.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromQuery] RegistrationDto newUser)
        {
            var user = await _authService.RegisterAsync(newUser);
            if (user == null)
            {
                return BadRequest("Registration failed.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            return Ok("Registration and login successful");
        }

        /// <summary>
        /// Авторизация пользователя в системе.
        /// </summary>
        /// <param name="dto">Данные для входа пользователя (Email и Password).</param>
        /// <returns>Результат входа пользователя.</returns>
        /// <response code="200">Успешный вход в систему.</response>
        /// <response code="400">Неверные данные для входа.</response>
        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] LoginDto dto)
        {
            var user = await _authService.LoginAsync(dto);
            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            return Ok("Logged in successfully");
        }

        /// <summary>
        /// Выход пользователя из системы.
        /// </summary>
        /// <returns>Результат выхода из системы.</returns>
        /// <response code="200">Успешный выход из системы.</response>
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Logged out successfully");
        }
    }
}
