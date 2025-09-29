using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Request;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        public class SignUpConsumerRequest
        {
            public AuthRequest User { get; set; } = null!;
            public ConsumerSignUpRequest Consumer { get; set; } = null!;
        }

        public class SignUpAdminRequest
        {
            public AuthRequest User { get; set; } = null!;
            public AdminSignUpRequest Admin { get; set; } = null!;
        }

        [HttpPost("signup/consumer")]
        public async Task<IActionResult> SignUpConsumer([FromBody] SignUpConsumerRequest request)
        {
            if (request == null || request.User == null || request.Consumer == null)
                return BadRequest("Invalid request payload.");

            try
            {
                var createdUserId = await _userService.SignUpConsumerAsync(request.User, request.Consumer);

                return Ok(new
                {
                    Message = "Consumer signed up successfully",
                    UserId = createdUserId
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred during signup." });
            }
        }

        [HttpPost("signup/admin")]
        public async Task<IActionResult> SignUpAdmin([FromBody] SignUpAdminRequest request)
        {
            if (request == null || request.User == null || request.Admin == null)
                return BadRequest("Invalid request payload.");

            try
            {
                var createdUserId = await _userService.SignUpAdminAsync(request.User, request.Admin);

                return Ok(new
                {
                    Message = "Admin signed up successfully",
                    UserId = createdUserId
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred during signup." });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { Message = "Username and password are required." });

            try
            {
                var loginResponse = await _userService.LoginAsync(request);

                if (loginResponse == null)
                {
                    return Unauthorized(new { Message = "Invalid username or password." });
                }

                return Ok(loginResponse);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred during login." });
            }
        }
    }
}
