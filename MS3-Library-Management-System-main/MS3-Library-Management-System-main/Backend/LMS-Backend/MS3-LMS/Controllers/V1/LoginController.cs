using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MS3_LMS.Enity.User;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MS3_LMS.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _Userservice;
        private readonly IConfiguration _configuration;

        public LoginController(IUserService userservice,IConfiguration configuration)
        {
            _Userservice = userservice;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var data=await _Userservice.Login(model.Email, model.Password);
            return Ok(data);
        }



       
    }




    
    
}
