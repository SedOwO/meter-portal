using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Security.Principal;
using WebApi.Data.Implementatoins;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Models.Response;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Implementations;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(Roles = "consumer")]
    public class ConsumerController : ControllerBase
    {
        private readonly IConsumerService _consumerService;
        private readonly IRechargeService _rechargeService;
        public ConsumerController(IConsumerService consumerService)
        {
            _consumerService = consumerService;
        }

        [HttpGet("complaints/{complaintId}")]
        public async Task<IActionResult> GetComplaintById(int complaintId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            try
            {
                var complaint = await _consumerService.GetComplaintByIdAsync(userId, complaintId);

                if (complaint == null)
                    return NotFound(new { Message = "Complaint not found or access denied" });

                return Ok(complaint);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred", Detail = ex.Message });
            }
        }

        [HttpPost("complaints")]
        public async Task<IActionResult> CreateComplaint([FromBody] CreateComplaintRequest request)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            try
            {
                var response = await _consumerService.CreateComplaintAsync(userId, request);

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

        [HttpGet("complaints")]
        public async Task<IActionResult> GetMyComplaints()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            try
            {
                var complaints = await _consumerService.GetUserComplaintAsync(userId);
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

        [HttpPost("recharge")]
        public async Task<IActionResult> RechargeSmartMeter([FromBody] RechargeRequest recharge)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            if (recharge.Amount <= 0)
            {
                return BadRequest(new { Message = "Recharge amount must be greater than zero" });
            }

            try
            {
                var response = await _rechargeService.RechargeSmartMeterAsync(userId, recharge.Amount);
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
                return StatusCode(500, new { Message = "An error occurred while processing recharge", Detail = ex.Message });
            }
        }
    }
}
