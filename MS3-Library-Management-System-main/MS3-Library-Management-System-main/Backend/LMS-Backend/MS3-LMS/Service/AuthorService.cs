﻿using MS3_LMS.Enity.Book;
using MS3_LMS.IRepository;
using MS3_LMS.IService;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;

namespace MS3_LMS.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IAothorRepository _aothorRepository;

        public AuthorService(IAothorRepository aothorRepository)
        {
            _aothorRepository = aothorRepository;
        }



        public async Task PostNewAuthor(AuthorRequestModel authorRequestModel)
        {
            try
            {
                var author = new Author
                {
                    Name = authorRequestModel.AuthorName,
                    ImageUrl = authorRequestModel.ImageUrl,
                    Bio = authorRequestModel.Bio,
                };

                await _aothorRepository.PostNewAuthor(author);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

        public async Task<List<AuthorResponseModel>> GelAuthors()
        {
            try
            {
                var dats = await _aothorRepository.GetAllAuthor();

                var response = dats.Select(s => new AuthorResponseModel
                {
                    AuthorId = s.AuthorId,
                    AuthorName = s.Name,
                    Bio = s.Bio,
                    ImageUrl = s.ImageUrl,
                }).ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    
}

