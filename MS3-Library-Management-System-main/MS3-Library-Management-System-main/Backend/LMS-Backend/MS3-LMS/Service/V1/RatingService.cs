using MS3_LMS.Enity.Book;
using MS3_LMS.IRepository;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;

namespace MS3_LMS.Service.V1
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _repository;

        public RatingService(IRatingRepository repository)
        {
            _repository = repository;
        }

        public async Task<Rating> PostRatingAsync(RatingRequestModel ratingRequestModel)
        {
            {
                try
                {
                    var request = new Rating
                    {
                        StarCount = ratingRequestModel.StarCount,
                        FeedBack = ratingRequestModel.FeedBack,
                        MemebID = ratingRequestModel.MemebID,
                        Bookid = ratingRequestModel.Bookid
                    };
                    var data = await _repository.PostRating(request);
                    return data;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
