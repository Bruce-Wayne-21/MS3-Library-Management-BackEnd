using MS3_LMS.Enity.Book;

namespace MS3_LMS.IRepository.V1
{
    public interface ILanguageRepository
    {
        Task<List<Language>> GetallLanguage();
    }
}
