using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_LMS.Enity.Core;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;
using System.ComponentModel.DataAnnotations;

namespace MS3_LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooklendController : ControllerBase
    {
        private readonly IBookLendService _bookLendService;

        public BooklendController(IBookLendService bookLendService)
        {
            _bookLendService = bookLendService;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BookLendRequest request)
        {
            try
            {
                var bookLend = await _bookLendService.BookRequest(request);
                return Ok(new { success = true, message = "Book borrowed successfully.", data = bookLend });
            }
            catch (InvalidOperationException ex)
            {
                
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { success = false, message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRequest()
        {
            try
            {
                var response = await _bookLendService.GetBookLendResponsesAsync();
                if (response == null)
                {
                    return BadRequest("there are no request");
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("Booklend/Status/{state}")]
        public async Task<IActionResult>GetRecords(BookLend.State state)
        {
            try
            {
                var data = await _bookLendService.EnumBasedResponse(state);
                if(data == null)
                {
                    return BadRequest("Records Are Empty");
                }
                return Ok(data);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        [HttpPut]
        public async Task<IActionResult>UpdateState(Guid id, BookLend.State state)
        {
            try
            {
                var data = await _bookLendService.UpdateRecord(id, state);
                if(data == null)
                {
                    return BadRequest();
                }
                return Ok(data);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        

        [HttpPut("updateDate")]
        public async Task<IActionResult>UpdateDate(Guid MemberID,DateTime Date,string Datetype)
        {
            try
            {
                var data = await _bookLendService.UpdateDates(MemberID, Date, Datetype);
                if (data)
                {
                    return Ok("date is updated");
                }
                return Ok("date is did not updated");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [HttpGet("GetRecervedBook")]
        public async Task <IActionResult>GetReservedBook(Guid MemberID,BookLend.State state)
        {
            try
            {
                var data = await _bookLendService.GetDetailsByMemberId(MemberID, state);
                if(data == null)
                {
                    return BadRequest("Book not found");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

         
        
        

        
    }
}
