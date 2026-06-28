using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;

namespace MS3_LMS.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("AddNewImage")]
        public async Task<IActionResult>NewImage(ImageRequestModel imageRequestModel)
        {
            try
            {
                 await _imageService.AddNewBook(imageRequestModel);
                return Ok("book Image success Fully Assign");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
