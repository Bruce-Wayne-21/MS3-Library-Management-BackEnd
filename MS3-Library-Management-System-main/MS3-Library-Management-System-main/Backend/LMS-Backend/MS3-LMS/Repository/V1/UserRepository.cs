using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using MS3_LMS.Enity.User;
using MS3_LMS.IRepository.V1;
using MS3_LMS.LMSDbcontext;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace MS3_LMS.Repository.V1
{
    public class UserRepository:IUserRepository
    {
        private readonly LMSContext _Context;

        public UserRepository(LMSContext context)
        {
            _Context = context;
        }

        public async Task<User> createUser(User user)  
        {
            var data= await _Context.Users.AddAsync(user);
            await _Context.SaveChangesAsync();
            return data.Entity;
            
        }
        public async Task createMemeber(Member member)
        {
              await _Context.Members.AddAsync(member);
            await _Context.SaveChangesAsync();
        }

        public async Task CreteAdmin(User user)
        {
            await _Context.Users.AddAsync(user);
            await _Context.SaveChangesAsync();
        }


        public async Task<User?> LoginAsync(string email, string password)
        {
            try
            {
                
                var user = await _Context.Users
                    .Include(s => s.UserRoles)
                    .ThenInclude(r => r.Role)
                    .FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return null;
                }
                if (user.IsConfirmEmail == null || user.IsConfirmEmail == false)
                {
                    throw new InvalidOperationException("Email not confirmed. Please verify your email.");
                }
                if (string.IsNullOrEmpty(user.PasswordHash) || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return null;
                }
                return user;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing your login. Please try again.", ex);
            }
        }


        public async Task<Member> GetMemberByUSerId(Guid userId)
        {
            try
            {
                var data = await _Context.Members.FirstOrDefaultAsync(m => m.UserId == userId);

                if (data == null)
                {
                    throw new KeyNotFoundException($"Member with UserId {userId} was not found.");
                }

                return data;
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.Error.WriteLine($"Error in GetMemberByUSerId: {ex.Message}");
                throw; // Rethrow the original exception
            }
        }





    }
}
