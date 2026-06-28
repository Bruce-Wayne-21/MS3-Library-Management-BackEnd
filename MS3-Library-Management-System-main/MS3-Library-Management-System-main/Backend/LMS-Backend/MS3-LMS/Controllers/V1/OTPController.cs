using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_LMS.Models.RequestModel;

namespace MS3_LMS.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class OTPController : ControllerBase
    {
        private readonly OTPService _otpService;

        public OTPController(OTPService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("SendOtp")]
        public async Task<IActionResult> SendOtpAsync([FromBody] SendOtpRequest request)
        {
            try
            {
                var result = await _otpService.GenerateAndSendOtpAsync(request.UserId, request.Email);
                if (result)
                    return Ok(new { message = "OTP sent successfully." });

                return BadRequest(new { message = "Failed to send OTP." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] OtprequestModel request)
        {
            try
            {
                var isVerified = await _otpService.VerifyOtpAsync(request.UserId, request.OTPCode);

                if (isVerified)
                {
                    return Ok(new { Message = "OTP verified successfully." });
                }

                return BadRequest(new { Message = "OTP verification failed." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }




    public class SendOtpRequest
    {
        public string Email { get; set; }

        public Guid UserId { get; set; }


    }

}
