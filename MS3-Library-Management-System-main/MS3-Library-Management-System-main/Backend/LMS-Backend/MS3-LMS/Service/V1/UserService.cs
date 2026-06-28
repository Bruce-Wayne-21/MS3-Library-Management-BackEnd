using Microsoft.IdentityModel.Tokens;
using MS3_LMS.Enity.User;
using MS3_LMS.IRepository;
using MS3_LMS.IRepository.V1;
using MS3_LMS.IService.V1;
using MS3_LMS.Models.RequestModel;
using MS3_LMS.Models.ResponeModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;


namespace MS3_LMS.Service.V1
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleService _roleService;
        private readonly IMemberRepository _memberRepository;
        private readonly IConfiguration _configuration;
      


        public UserService(IUserRepository  userRepository, IRoleRepository roleRepository, IRoleService roleService,IMemberRepository memberRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _roleService = roleService;
            _memberRepository = memberRepository;
            _configuration = configuration;
        }

        public async Task NewAdmin(UserRequestModel  userRequestModel)
        {
            try
            {
                var admin = new User
                {
                    Email = userRequestModel.Email,
                    IsConfirmEmail = true,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRequestModel. password),
                
                };
                await _userRepository.CreteAdmin(admin);


                //var admin = new Member
                //{
                //    Nic = memberRequestModel.Nic,
                //    Email = memberRequestModel.Email,
                //    FirstName = memberRequestModel.FirstName,
                //    LastName = memberRequestModel.LastName,
                //    PhoneNumber = memberRequestModel.PhoneNumber,
                //    ImageUrl = memberRequestModel.ImageUrl,
                //    UserGender= memberRequestModel.UserGender,
                //    UserId=user.UserId
                //};

                //await _memberRepository.CreateNewUser(admin);

                await _roleService.AssignAdmin(admin.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex. Message);
            }



        }

        public async Task<LoginResponseModel> Login(string email, string password)
        {
            try
            {
                
                var user = await _userRepository.LoginAsync(email, password);

                if (user == null)
                {
                    throw new UnauthorizedAccessException("Invalid Email or Password.");
                }

              
                var roles = user.UserRoles.Select(ur => ur.Role.UserAType).ToList();

               
                var token = createToken(user, roles);

                return new LoginResponseModel
                {
                    Token = token
                };
            }
            catch (InvalidOperationException ex)
            {
                
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
               
                throw new Exception("An error occurred during login: " + ex.Message);
            }
        }





        public string createToken(User user, List<string> Roles)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creadentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>
            {
               new Claim (JwtRegisteredClaimNames.Sub,user.Email),

               new Claim(JwtRegisteredClaimNames.Jti,Guid .NewGuid().ToString()),
               new Claim (ClaimTypes.NameIdentifier,user.UserId.ToString()),
               new Claim (ClaimTypes.Email,user.Email)

            }; 

            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                 expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:TokenExpiryInMinutes"])),
                signingCredentials: creadentials

             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<MemberIDRequestModel> GetByUserID(Guid userId)
        {
            try
            {
                var data = await _userRepository.GetMemberByUSerId(userId);
                if (data == null)
                {
                    throw new KeyNotFoundException("Member Not Found");
                }

                return new MemberIDRequestModel
                {
                    MemberID = data.MemebID
                };
            }
            catch (KeyNotFoundException ex)
            {
                // Log the error
                Console.Error.WriteLine($"Error in GetByUserID: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log other unexpected errors
                Console.Error.WriteLine($"Unexpected error in GetByUserID: {ex.Message}");
                throw new Exception("An unexpected error occurred. Please contact support.");
            }
        }






    }
}
