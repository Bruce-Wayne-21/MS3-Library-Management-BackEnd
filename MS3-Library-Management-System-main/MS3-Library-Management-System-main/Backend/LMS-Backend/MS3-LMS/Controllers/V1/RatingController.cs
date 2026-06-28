using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS3_LMS.IService.V1;
using MS3_LMS.LMSDbcontext;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Repository;

namespace MS3_LMS.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        private readonly LMSContext _context;

        public RatingController(IRatingService ratingService,LMSContext lMSContext)
        {
            _ratingService = ratingService;
            _context = lMSContext;
        }


        [HttpPost]
        public async Task<IActionResult>Rating(RatingRequestModel ratingRequestModel)
        {
            try
            {
                var data = await _ratingService.PostRatingAsync(ratingRequestModel);
                if (data != null)
                {
                    return Ok(data);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{bookid}")]
        public async Task <IActionResult>getRating(Guid bookid)
        {
            try
            {
                var ratings = await _context.Rating.Include(m=>m.Member).Where(r => r.Bookid == bookid).ToListAsync();
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Summery{bookId}")]
        public async Task<IActionResult> GetRatingSummary(Guid bookId)
        {
            var ratings = await _context.Rating.Where(r => r.Bookid == bookId).ToListAsync();

            if (!ratings.Any())
            {
                return Ok(new { AverageRating = 0, StarDistribution = new int[5] });
            }

            var averageRating = ratings.Average(r => (decimal)(r.StarCount ?? 0));
            var starDistribution = ratings
                .GroupBy(r => r.StarCount)
                .ToDictionary(g => g.Key, g => g.Count());

            return Ok(new
            {
                AverageRating = Math.Round(averageRating, 1),
                StarDistribution = Enumerable.Range(1, 5)
                    .Select(i => starDistribution.ContainsKey(i) ? starDistribution[i] : 0)
                    .ToArray()
            });
        }

        [HttpDelete("deleteRating")]
        public async Task<IActionResult>DeleteRating(Guid id)
        {
            try
            {
                var data = await _context.Rating.FindAsync(id);
                 _context.Remove(data);
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

    }
}
