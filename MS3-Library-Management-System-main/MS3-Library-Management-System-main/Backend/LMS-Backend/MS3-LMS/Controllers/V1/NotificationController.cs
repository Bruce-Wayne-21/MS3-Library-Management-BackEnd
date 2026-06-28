using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_LMS.IRepository;
using MS3_LMS.IService.V1;

namespace MS3_LMS.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IBookLendService _bookLendService;

        public NotificationController(IBookLendService bookLendService)
        {
            _bookLendService = bookLendService;
        }


        [HttpPost("OverDueNotification")]
        public async Task<IActionResult> SendOverDueWarning()
        {
            try
            {
                 await _bookLendService.SendNotification();
                return Ok("Over Due sent Sucesss");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("SentNotifi")]
        public async Task<IActionResult> SentNotification(Guid MemberID)
        {
            try
            {
                var data = await _bookLendService.sentNotification(MemberID);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
