using MS3_LMS.Enity.Book;
using MS3_LMS.IRepository;
using MS3_LMS.LMSDbcontext;

namespace MS3_LMS.Repository.V1
{
    public class ImageRepository:IImageRepository
    {
        private readonly LMSContext _context;

        public ImageRepository(LMSContext context)
        {
            _context = context;
        }

        public async Task<Image>AddBookImage(Image image)
        {
            try
            {
                var imag = await _context.Images.AddAsync(image);
                await _context.SaveChangesAsync();
                return imag.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
