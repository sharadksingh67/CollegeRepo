using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartCollege.Services.College.Models.Dto;
using SmartCollege.Services.College.Services;

namespace SmartCollege.Services.College.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            return result == null ? Unauthorized() : Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequestDto request)
        {
            var result = await _authService.RefreshAsync(request.RefreshToken);
            return result == null ? Unauthorized() : Ok(result);
        }

        [HttpGet("hash/{password}")]
        [AllowAnonymous]
        public IActionResult GetHash(string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            return Ok(hash);
        }

    }
}
