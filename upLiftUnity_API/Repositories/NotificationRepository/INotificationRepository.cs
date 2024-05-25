using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.NotificationRepository
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetAllAsync();
        Task CreateNotificationAsync(Notification notification);
        Task UpdateIsReadAsync(Guid notificationId, bool isRead);
    }
}
