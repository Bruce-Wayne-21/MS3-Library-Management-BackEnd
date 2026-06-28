using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("NewAuthor")]
        public async Task <IActionResult>PostNewAuthor(AuthorRequestModel authorRequestModel)
        {
           await _authorService.PostNewAuthor(authorRequestModel);
            return Ok(authorRequestModel);
        }

        [HttpGet("AllAuthors")]
        public async Task<IActionResult>AllAuthors()
        {
            try
            {
                var data = await _authorService.GelAuthors();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAuthorsBook")]
        public async Task<IActionResult>GetAllBooksByAuthors(Guid authorID)
        {
            try
            {
                var data = await _authorService.GetAuthorsBooksById(authorID);
                if(data == null)
                {
                    return BadRequest("No books Available");

                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
