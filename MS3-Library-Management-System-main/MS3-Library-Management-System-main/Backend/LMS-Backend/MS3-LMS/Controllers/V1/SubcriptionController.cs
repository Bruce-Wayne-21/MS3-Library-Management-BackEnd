using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Service;

namespace MS3_LMS.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcriptionController : ControllerBase
    {
        private readonly ISubcriptionService _subcriptionService;

        public SubcriptionController(ISubcriptionService subcriptionService)
        {
            _subcriptionService = subcriptionService;
        }

        [HttpPost]
        [Route("NewSubcription")]
        public async Task<IActionResult> NewSubcription(SubcriptionRequest subcriptionRequest)
        {
            try
            {
                var data = await _subcriptionService.NewSubcription(subcriptionRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        [HttpGet("IsActive/{memberId}")]
        public async Task<IActionResult> IsActive(Guid memberId)
        {
            try
            {
                var isActive = await _subcriptionService.CheckIsActive(memberId);

                if (!isActive)
                {
                    return NotFound(new { Message = "No active subscription found for the member." });
                }

                return Ok(isActive);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while checking the subscription status.", Details = ex.Message });
            }
        }

        [HttpGet("allSubscription")]
        public async Task<IActionResult> AllSubscrip()
        {
            try
            {
                var data = await _subcriptionService.GetSubCription();
                return Ok(data);
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
