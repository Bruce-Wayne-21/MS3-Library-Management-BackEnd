using MS3_LMS.Enity.Book;


namespace MS3_LMS.IRepository
{
    public interface IImageRepository
    {
        Task<Image> AddBookImage(Image image);
    }
}
