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
            public UserSignUpRequest User { get; set; } = null!;
            public ConsumerSignUpRequest Consumer { get; set; } = null!;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpConsumer([FromBody] SignUpConsumerRequest request)
        {
            if (request == null || request.User == null || request.Consumer == null)
                return BadRequest("Invalid request payload.");

            var createdUserId = await _userService.SignUpConsumerAsync(request.User, request.Consumer);

            return Ok(new
            {
                Message = "Consumer signed up successfully",
                UserId = createdUserId
            });
        }

    }
}
