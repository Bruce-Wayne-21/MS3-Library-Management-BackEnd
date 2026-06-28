using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpPost]
        public async Task <IActionResult>PostGenre(GenreRequestModel model)
        {
            try
            {
                 await _genreService.CreteGenre(model);
                return Ok("genre created sucees");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllGenres")]
        public async Task<IActionResult> AllGenres()
        {
            try
            {
               
                var data = await _genreService.GetAllGenres();

                
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"Error in AllGenres: {ex.Message}");
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

    }
}
