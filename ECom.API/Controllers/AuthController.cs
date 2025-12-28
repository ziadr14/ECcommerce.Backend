using ECom.BLL.DTOs;
using ECom.BLL.DTOs.Pagination;
using ECom.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECom.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var success = await _authService.RegisterAsync(dto);

            if (!success)
            {
                return BadRequest(new
                {
                    message = "Username or Email already exists"
                });
            }

            return Ok(new
            {
                message = "Registered successfully, please check your email"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);

            if (token == null)
            {
                return BadRequest(new
                {
                    message = "Invalid email or password"
                });
            }

            return Ok(new
            {
                message = "Login successful",
                token
            });
        }

        [HttpPost("active-account")]
        public async Task<IActionResult> ActivateAccount([FromBody] ActivateAccountDto dto)
        {
            var result = await _authService.ActivateAccountAsync(dto);

            if (!result)
                return BadRequest(new { message = "Invalid or expired activation link" });

            return Ok(new { message = "Email confirmed successfully" });
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            await _authService.ForgotPasswordAsync(dto);

            return Ok(new
            {
                message = "If this email exists, a reset link was sent"
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var success = await _authService.ResetPasswordAsync(dto);

            if (!success)
            {
                return BadRequest(new
                {
                    message = "Invalid reset token or password"
                });
            }

            return Ok(new
            {
                message = "Password reset successfully"
            });
        }

    }
}
