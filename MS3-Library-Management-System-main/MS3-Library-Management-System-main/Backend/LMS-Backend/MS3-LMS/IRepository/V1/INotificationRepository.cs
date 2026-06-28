namespace MS3_LMS.IRepository.V1
{
    public interface INotificationRepository
    {
        Task AddNotification(Enity.Notification.Notification notification);
        Task<List<Enity.Notification.Notification>> GetNotificationAsync(Guid MemberID);
    }
}
