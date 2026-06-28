using Microsoft.CodeAnalysis.Host;
using MS3_LMS.IRepository.V1;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.Service.V1
{
    public class LanguageService:IService.V1.ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<List<LanguageResponseModel>> GetallLanguage()
        {
            try
            {
                var language = await _languageRepository.GetallLanguage();

                var response = language.Select(s => new LanguageResponseModel
                {
                    LanguageId = s.LanguageId,
                    TypeOfLanguage = s.TypeOfLanguage
                }).ToList();
                if(response == null)
                {
                    throw new Exception("no language available");
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
