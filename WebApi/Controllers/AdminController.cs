using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApi.Models.Misc;
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
        public async Task<IActionResult> GetAllComplaintsPaginated([FromQuery] PaginationParams? paginationParams)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return Unauthorized(new { Message = "Invalid user" });
            }

            try
            {
                if (paginationParams == null || paginationParams.Page <= 0 || paginationParams.PageSize <= 0)
                {
                    var complaints = await _adminService.GetAllComplaints();
                    return Ok(complaints);
                }

                var pagedComplaints = await _adminService.GetAllComplaintsPaginated(paginationParams.Page, paginationParams.PageSize);

                Response.Headers.Append("X-Pagination-CurrentPage", pagedComplaints.Page.ToString());
                Response.Headers.Append("X-Pagination-PageSize", pagedComplaints.PageSize.ToString());
                Response.Headers.Append("X-Pagination-TotalCount", pagedComplaints.TotalCount.ToString());
                Response.Headers.Append("X-Pagination-TotalPages", pagedComplaints.TotalPages.ToString());
                Response.Headers.Append("X-Pagination-HasNext", pagedComplaints.HasNextPage.ToString());
                Response.Headers.Append("X-Pagination-HasPrevious", pagedComplaints.HasPreviousPage.ToString());

                return Ok(new
                {
                    items = pagedComplaints.Items,
                    paginaiton = new
                    {
                        page = pagedComplaints.Page,
                        pageSize = pagedComplaints.PageSize,
                        totalCount = pagedComplaints.TotalCount,
                        totalPages = pagedComplaints.TotalPages,
                        hasNextPage = pagedComplaints.HasNextPage,
                        hasPreviousPage = pagedComplaints.HasPreviousPage
                    }
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error occurred",
                    Detail = ex.Message,
                    StackTrace = ex.StackTrace
                });
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
