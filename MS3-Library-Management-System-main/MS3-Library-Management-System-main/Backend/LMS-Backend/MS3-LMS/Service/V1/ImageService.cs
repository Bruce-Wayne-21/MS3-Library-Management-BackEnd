using MS3_LMS.Enity.Book;
using MS3_LMS.IRepository;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;

namespace MS3_LMS.Service.V1
{
    public class ImageService:IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task AddNewBook(ImageRequestModel imageRequestModel)
        {
            try
            {
                var request = new Image
                {
                    Image1Path = imageRequestModel.Image1Path,
                    Image2Path = imageRequestModel.Image2Path,
                    Bookid = imageRequestModel.Bookid
                };
                await _imageRepository.AddBookImage(request);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
