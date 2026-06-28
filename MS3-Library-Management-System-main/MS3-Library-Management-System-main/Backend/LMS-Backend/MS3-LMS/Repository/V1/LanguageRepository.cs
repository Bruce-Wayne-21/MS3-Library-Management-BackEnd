using Microsoft.EntityFrameworkCore;
using MS3_LMS.Enity.Book;
using MS3_LMS.IRepository.V1;
using MS3_LMS.LMSDbcontext;

namespace MS3_LMS.Repository.V1
{
    public class LanguageRepository: ILanguageRepository
    {
        private readonly LMSContext _context;

        public LanguageRepository(LMSContext context)
        {
            _context = context;
        }
        
        public async Task<List<Language>> GetallLanguage()
        {
            try
            {
                var data = await _context.Languages.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
