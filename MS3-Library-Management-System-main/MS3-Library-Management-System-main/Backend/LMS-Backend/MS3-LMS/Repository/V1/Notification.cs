using Microsoft.EntityFrameworkCore;
using MS3_LMS.IRepository.V1;
using MS3_LMS.LMSDbcontext;

namespace MS3_LMS.Repository.V1
{
    public class Notification : INotificationRepository
    {
        private readonly LMSContext _context;

        public Notification(LMSContext context)
        {
            _context = context;
        }

        public async Task AddNotification(Enity.Notification.Notification notification)
        {
            if (notification == null)
                throw new ArgumentNullException(nameof(notification), "Notification object is null. Ensure proper instantiation before calling this method.");

            try
            {
                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding notification: {ex.Message}");
            }
        }


        public async Task<List<Enity.Notification.Notification>> GetNotificationAsync(Guid MemberID)
        {
            try
            {

                var data = await _context.Notifications
                    .Where(i => i.ReceiveId == MemberID && (i.ViewStatus == false || i.ViewStatus == null))
                    .ToListAsync();
                if (data == null || !data.Any())
                {
                    return new List<Enity.Notification.Notification>();
                }

                return data;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error fetching notifications: {ex.Message}");
            }
        }













    }
}
