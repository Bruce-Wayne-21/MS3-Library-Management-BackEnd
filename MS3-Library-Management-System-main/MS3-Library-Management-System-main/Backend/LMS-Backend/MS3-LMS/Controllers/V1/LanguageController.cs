using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host;

namespace MS3_LMS.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
       private readonly IService.V1.ILanguageService _languageService;

        public LanguageController(IService.V1.ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet("AllLanguage")]
        public async Task<IActionResult> GetAllLanguage()
        {
            try
            {
                var data = await _languageService.GetallLanguage();
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
