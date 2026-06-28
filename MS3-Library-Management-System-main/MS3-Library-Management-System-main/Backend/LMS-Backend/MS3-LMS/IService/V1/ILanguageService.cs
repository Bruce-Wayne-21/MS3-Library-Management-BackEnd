using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.IService.V1
{
    public interface ILanguageService
    {
        Task<List<LanguageResponseModel>> GetallLanguage();
    }
}
