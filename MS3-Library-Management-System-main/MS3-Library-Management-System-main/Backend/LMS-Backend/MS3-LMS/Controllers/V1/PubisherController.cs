using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PubisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PubisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpPost("NewPublisher")]
        public async Task<IActionResult>postPublisher(PublisherRequestModel publisherRequestModel)
        {
            try
            {
                await _publisherService.CretePublisher(publisherRequestModel);
                return Ok("publisher created");
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        [HttpGet("AllPublisher")]
        public async Task<IActionResult> AllPublishers()
        {
            try
            {
                var data = await _publisherService.GetAllPublishers();
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
