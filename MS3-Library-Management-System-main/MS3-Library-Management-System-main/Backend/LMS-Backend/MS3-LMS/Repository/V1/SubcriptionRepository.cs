using Microsoft.EntityFrameworkCore;
using MS3_LMS.Enity.Core;
using MS3_LMS.IRepository.V1;
using MS3_LMS.LMSDbcontext;

namespace MS3_LMS.Repository.V1
{
    public class SubcriptionRepository: ISubcriptionRepository
    {
        private readonly LMSContext _context;

        public SubcriptionRepository(LMSContext context)
        {
            _context = context;
        }


        public async Task<Subscription>NewSubcription(Subscription subscription)
        {
            try
            {
                var data=await _context.Subscriptions.AddAsync(subscription);
                await _context.SaveChangesAsync();
                return data.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }


        public async Task<Subscription?> CheckSubIsActive(Guid memberId)
        {
            try
            {
                return await _context.Subscriptions
                                     .SingleOrDefaultAsync(sub => sub.MemebID == memberId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving subscription data.", ex);
            }
        }

        public async Task<List<Subscription>> GetAllSubcription()
        {
            try
            {
                var data=await _context.Subscriptions.Include(m=>m.Member).Include(p=>p.Payment).AsNoTracking().ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        
    }
}
