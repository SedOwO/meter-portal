using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
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
        private readonly ISmartMeterRepository _smartMeterRepository;
        private readonly IProfileService _profileService;
        private readonly INotificationService _notificationService;
        public ConsumerController(INotificationService notificationService, IConsumerService consumerService, IRechargeService rechargeService, ISmartMeterRepository smartMeterRepository, IProfileService profileService)
        {
            _consumerService = consumerService;
            _rechargeService = rechargeService;
            _smartMeterRepository = smartMeterRepository;
            _profileService = profileService;
            _notificationService = notificationService;
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
                var response = await _rechargeService.RechargeSmartMeterAsync(userId, recharge);
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

        [HttpGet("recharge/history")]
        public async Task<IActionResult> GetRechargeHistory()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            try
            {
                var history = await _rechargeService.GetUserRechargeHistoryAsync(userId);
                return Ok(history);
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

        [HttpGet("smartmeter")]
        public async Task<IActionResult> GetSmartMeter()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            var consumer = await _profileService.GetProfileByIdAsync(userId);

            try
            {
                var meters = await _smartMeterRepository.GetAllMetersByConsumerId(consumer.ConsumerId);
                return Ok(meters);
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

        [HttpGet("notifications")]
        public async Task<IActionResult> GetUnreadNotifications()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            try
            {
                var notifications = await _notificationService.GetUnreadNotificationAsync(userId);
                return Ok(notifications);

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

        [HttpPut("notifications/{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            try
            {
                await _notificationService.MarkNotificationAsRead(userId, notificationId);
                return Ok();
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
    }
}
