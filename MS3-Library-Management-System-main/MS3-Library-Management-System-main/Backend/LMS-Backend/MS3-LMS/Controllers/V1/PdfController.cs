using Microsoft.AspNetCore.Mvc;
using UglyToad.PdfPig;

namespace MS3_LMS.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        public class FilePathRequest
        {
            public string FilePath { get; set; }
        }

        [HttpPost("extract")]
        public IActionResult ExtractText([FromBody] FilePathRequest request)
        {
            if (string.IsNullOrEmpty(request.FilePath))
            {
                return BadRequest("File path is required.");
            }

            try
            {
                if (!System.IO.File.Exists(request.FilePath))
                {
                    return NotFound("The specified file does not exist.");
                }

                using var stream = System.IO.File.OpenRead(request.FilePath);
                using var pdfDocument = PdfDocument.Open(stream);

                var extractedText = new List<string>();

                // Extract text from all pages
                foreach (var page in pdfDocument.GetPages())
                {
                    extractedText.Add(page.Text);
                }

                var result = new
                {
                    FilePath = request.FilePath,
                    PageCount = pdfDocument.NumberOfPages,
                    Content = extractedText
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
