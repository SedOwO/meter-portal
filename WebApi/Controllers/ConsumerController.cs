using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(Roles = "consumer")]
    public class ConsumerController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ConsumerController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfileAsync()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized("Invalid user");
            }

            try
            {
                var profile = await _profileService.GetProfileByIdAsync(userId);
                return Ok(profile);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return Problem(
                   detail: e.Message,
                   title: "Internal Server Error"
               );
            }
        }
    }
}
