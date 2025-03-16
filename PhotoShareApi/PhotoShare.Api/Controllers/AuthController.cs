using Microsoft.AspNetCore.Mvc;
using PhotoShare.Core.DTOs;
using PhotoShare.Core.IServices;
using System;
using System.Threading.Tasks;
using PhotoShare.Api.PostModels;

namespace PhotoShare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                var createdUser = await _authService.RegisterUser(userDto);
                return CreatedAtAction(nameof(Register), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var token = await _authService.LoginUser(userLogin.Email, userLogin.Password);
                if (token == null)
                {
                    return Unauthorized();
                }

                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
