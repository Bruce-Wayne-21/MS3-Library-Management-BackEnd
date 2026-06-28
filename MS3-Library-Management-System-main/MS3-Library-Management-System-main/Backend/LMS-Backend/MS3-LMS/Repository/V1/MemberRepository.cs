using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.EntityFrameworkCore;
using MS3_LMS.Enity.Book;
using MS3_LMS.Enity.Core;
using MS3_LMS.Enity.User;
using MS3_LMS.IRepository.V1;
using MS3_LMS.LMSDbcontext;
using System.Security.Cryptography;

namespace MS3_LMS.Repository.V1
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LMSContext _context; 


        public MemberRepository(LMSContext context)
        {
            _context = context;
        }


        public async Task CreateNewUser(Member member) 
        {
            try
            {
                await _context.Members.AddAsync(member);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)   
            {

                throw new Exception($"An error occurred while creating the member: {ex.Message}", ex);
            }
        }
        public async Task<List<Member>> GetAllMembers()
        {
            try
            {
                var data = await _context.Members.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Member> GetMemberById(Guid MemberId)
        {
            try
            {
                var data = await _context.Members.FirstOrDefaultAsync(f=>f.MemebID==MemberId);
                if(data == null)
                {
                    throw new KeyNotFoundException("Member Not Found");
                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BookLend>> GetMemberBorrowedBook(Guid id, BookLend.State state)
        {
            try
            {
                
                var data = await _context.BookLends
                    .Where(bl => bl.MemebID == id && bl.Status == state)
                    .Include(bl => bl.Book) 
                    .Include(bl => bl.Book.Image) 
                    .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching borrowed books: {ex.Message}");
            }
        }


        public async Task<Member>UpdateMemberDetails( Member member)
        {
            try
            {
                 _context.Members.Update(member);
                await _context.SaveChangesAsync();
                return member;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteMemerByid(Member member)
        {
            try
            {
                if(member == null)
                {
                    throw new ArgumentNullException("no member delete");
                }
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }


        public async Task <bool>updatePAsswordAsync(Guid Userid,string HashPassword)
        {
            try
            {
                var pass=await _context.Users.FindAsync(Userid);
                if(pass == null)
                {
                    throw new Exception("no hash paaword");
                }
                pass.PasswordHash= HashPassword;
                     _context.Update(pass);
                await _context.SaveChangesAsync();
                return true;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




       
      


        







    }
}