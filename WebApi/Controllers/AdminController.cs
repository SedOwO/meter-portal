using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models.Request;
using WebApi.Repositories.Implementations;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ISmartMeterService _smartMeterService;

        public AdminController(IAdminService adminService, ISmartMeterService smartMeterService)
        {
            _adminService = adminService;
            _smartMeterService = smartMeterService;
        }

        [HttpGet("complaints")]
        public async Task<IActionResult> GetAllComplaints()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            try
            {
                var complaints = await _adminService.GetAllComplaints();
                return Ok(complaints);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Detail = ex.Message });
            }
        }

        [HttpPost("smartmeters")]
        public async Task<IActionResult> CreateSmartMeter([FromBody] SmartMeterRequest meter)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            try
            {
                var response = await _smartMeterService.CreateSmartMeterAsync(userId, meter);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating complaint", Detail = ex.Message });
            }
        }

        [HttpGet("smartmeter")]
        public async Task<IActionResult> GetSmartMeterByConsumerIdAsync([FromQuery] int? consumerId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            try
            {
                if (consumerId.HasValue)
                {
                    var meters = await _smartMeterService.GetAllSmartMeterByConsumerIdAsync(userId, consumerId.Value);

                    return Ok(meters);
                }

                var allMeters = await _smartMeterService.GetAllSmartMetersAsync(userId);

                return Ok(allMeters);               
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating smart meter", Detail = ex.Message });
            }
        }
    }
}
