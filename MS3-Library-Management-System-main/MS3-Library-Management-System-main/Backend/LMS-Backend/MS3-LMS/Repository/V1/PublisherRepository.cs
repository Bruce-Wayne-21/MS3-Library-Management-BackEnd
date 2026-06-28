using Microsoft.EntityFrameworkCore;
using MS3_LMS.Enity.Book;
using MS3_LMS.IRepository.V1;
using MS3_LMS.LMSDbcontext;

namespace MS3_LMS.Repository.V1
{
    public class PublisherRepository: IPublisherRepository
    {
        private readonly LMSContext _context;

        public PublisherRepository(LMSContext context)
        {
            _context = context;
        }

        public async Task <Publisher>PostPublisher(Publisher publisher)
        {
            try
            {
                var data = await _context.AddAsync(publisher);
                await _context.SaveChangesAsync();
                return data.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public  async Task<List<Publisher>>GetAllpublisger()
        {
            try
            {
               var data=  await _context.Publishers.ToListAsync();
                return data;
               


            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
