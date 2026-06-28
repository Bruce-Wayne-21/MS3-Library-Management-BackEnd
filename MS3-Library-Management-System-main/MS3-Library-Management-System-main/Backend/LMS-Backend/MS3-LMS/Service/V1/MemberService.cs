using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Identity.Client;
using MS3_LMS.Enity.Core;
using MS3_LMS.Enity.User;
using MS3_LMS.IRepository;
using MS3_LMS.IRepository.V1;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;
using MS3_LMS.Repository;
using NuGet.Protocol;

namespace MS3_LMS.Service.V1
{
    public class MemberService: IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleService _roleService;
        private readonly OTPService _otpService;


        public MemberService(IMemberRepository
            memberRepository, IUserRepository userRepository, IRoleRepository roleRepository, 
            IRoleService roleService,
            OTPService oTPService
            )
        {
            _memberRepository = memberRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _roleService = roleService;
            _otpService = oTPService;
        }

        //public async Task CreateNewUser(MemberRequestModel memberRequestModel)
        //{
        //    var member = new Member
        //    {
        //        MemebID = Guid.NewGuid(),
        //        Nic = memberRequestModel.Nic,
        //        FirstName = memberRequestModel.FirstName,
        //        LastName = memberRequestModel.LastName,
        //        Email = memberRequestModel.Email,
        //        UserGender = memberRequestModel.UserGender,
        //        ImageUrl = memberRequestModel.ImageUrl,
        //        IsVerify = false,
        //        //UserId = Guid.NewGuid(),
        //        //PhoneNumber = memberRequestModel.PhoneNumber,
        //    };

        //    await _memberRepository.CreateNewUser(member);
        //}


        public async Task<MemberResponse> NewMemeber(MemberRequestModel memberRequestModel)
        {

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Email = memberRequestModel.Email,
                IsConfirmEmail = false,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(memberRequestModel.Password)
            }; 
             await _userRepository.createUser(user);
            var member = new Member
            {   
                Nic = memberRequestModel.Nic,
                FirstName = memberRequestModel.FirstName,
                LastName = memberRequestModel.LastName,
                Email = memberRequestModel.Email,
                PhoneNumber = memberRequestModel.PhoneNumber,
                IsVerify = false,
                UserGender = memberRequestModel.UserGender,
                ImageUrl = memberRequestModel.ImageUrl,
                UserId = user.UserId ,
                RegisterDate= memberRequestModel.RegisterDate,
            };
               await _userRepository.createMemeber(member);
            await _roleService.AssignDefaultRole(user.UserId);

            var otpsent = await _otpService.GenerateAndSendOtpAsync(user.UserId, user.Email);
            if (!otpsent)
            {
                throw new Exception();
            }

            var response = new MemberResponse
            {
                userId = user.UserId,
                Nic = member.Nic,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                MemberID = member.MemebID,
                IsVerify = member.IsVerify,
                UserGender = member.UserGender,
                ImageUrl = member.ImageUrl,
                RegisterDate=member.RegisterDate
            };

            return response;
        }




        public async Task<List<MemberResponse>> GetAllMembers()
        {
            try
            {
                var members = await _memberRepository.GetAllMembers();

                
                var memberResponses = members.Select(member => new MemberResponse
                {
                    Nic = member.Nic,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Email = member.Email,
                    PhoneNumber = member.PhoneNumber,
                    UserGender = member.UserGender,
                    IsVerify=member.IsVerify ,
                    ImageUrl=member.ImageUrl,
                    RegisterDate=member.RegisterDate ,
                    MemberID=member.MemebID,
                    
                }).ToList();

                return memberResponses;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving members: " + ex.Message);
            }
        }



        public async Task <MemberResponse>GetMemberByNic(Guid Nic)
        {
            try
            {
                var member = await _memberRepository.GetMemberById(Nic);
                if(member == null)
                {
                    throw new Exception("Member Not Found ");
                }

                var response = new MemberResponse
                {
                    Nic = member.Nic,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Email = member.Email,
                    PhoneNumber = member.PhoneNumber,
                    UserGender = member.UserGender,
                    IsVerify=member.IsVerify,
                    ImageUrl=member.ImageUrl,
                    userId=member.UserId
                    
                };
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BookLendResponse>>GetRecordsById(Guid id ,BookLend.State state)
        {
            try
            {
                var data = await _memberRepository.GetMemberBorrowedBook(id, state);
                if(data == null)
                {
                    throw new Exception("Records Not Found");
                }
                var response = data.Select(s => new BookLendResponse
                {
                    Title=s.Book.Name,
                    Image1Path=s.Book.Image?.Image1Path,
                    Image2Path=s.Book.Image?.Image2Path

                }).ToList();
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MemberResponse>EditMember(Guid id,MemberResponse memberResponse)
        {
            try
            {
                var data = await _memberRepository.GetMemberById(id);
                if(data == null)
                {
                    throw new Exception();
                }
                
                data.FirstName = memberResponse.FirstName;
                data.LastName = memberResponse.LastName;
                data.Email = memberResponse.Email;
                data.UserGender = memberResponse.UserGender;
                data.ImageUrl = memberResponse.ImageUrl;

               var updateduser= await _memberRepository.UpdateMemberDetails(data);

                var response = new MemberResponse
                {
                    
                    FirstName = updateduser.FirstName,
                    LastName = updateduser.LastName,
                    Email = updateduser.Email,
                    UserGender = updateduser.UserGender,
                    ImageUrl = updateduser.ImageUrl,

                };
                return response;
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task <bool>DeleteMember(Guid id)
        {
            try
            {
                var membr = await _memberRepository.GetMemberById(id);
                if (membr == null)
                {
                    throw new Exception();
                }
                await _memberRepository.DeleteMemerByid(membr);
                return true;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

        public async Task<string> UpdateIsverify(Guid Memberid ,bool isverify)
        {
            try
            {
                var data = await _memberRepository.GetMemberById(Memberid);
                if (data == null)
                {
                    throw new Exception();
                }
                data.IsVerify = isverify;

                await _memberRepository.UpdateMemberDetails(data);

                return isverify ? "member verfify success full" : "member is not verify ";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool>updatePAssword(Guid userid,string password)
        {
            try
            {
                string hashword=BCrypt.Net.BCrypt.HashPassword(password);
                var data=await _memberRepository.updatePAsswordAsync(userid,hashword);
                return data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        

    }

}
