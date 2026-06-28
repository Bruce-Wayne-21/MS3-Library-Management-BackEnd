using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;

namespace MS3_LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userSerVice;

        public AdminController(IUserService userSerVice)
        {
            _userSerVice = userSerVice;
        }

        [HttpPost]
        public async Task<IActionResult>DefineAdmin(UserRequestModel userRequestModel)
        {
            await _userSerVice.NewAdmin(userRequestModel);

            return CreatedAtAction (nameof(DefineAdmin),new { id=userRequestModel.Email} ,userRequestModel);
        }
    }

    

}
