using MS3_LMS.Enity.Book;

namespace MS3_LMS.IRepository.V1
{
    public interface IPublisherRepository
    {
        Task<Publisher> PostPublisher(Publisher publisher);
        Task<List<Publisher>> GetAllpublisger();
    }
}
