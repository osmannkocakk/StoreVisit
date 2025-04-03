using Api.Services.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var token = await _authService.AuthenticateUser(request.Username);
            if (token == null)
                return Unauthorized(new { Message = "Invalid username" });

            return Ok(new { Token = token });
        }
    }


}
