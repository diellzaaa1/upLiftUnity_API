using upLiftUnity_API.Models;
using upLiftUnity_API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using upLiftUnity_API.Repositories.UserRepository;
using upLiftUnity_API.DTOs.NotificationDtos;

namespace upLiftUnity_API.Services
{
    public class NotificationServ
    {
        private readonly IMongoCollection<Notification> _notifications;


        public NotificationServ(IMongoDatabase database)
        {
            _notifications = database.GetCollection<Notification>("notifications");
   
        }

        public async Task<Notification> GetNotificationByIdAsync(string id)
        {
            return await _notifications.Find(notification => notification.NotificationId == Guid.Parse(id)).FirstOrDefaultAsync();
        }

        public async Task CreateNotificationAsync(Notification notification)
        {
            await _notifications.InsertOneAsync(notification);
        }

        public async Task UpdateNotificationAsync(string id, Notification updatedNotification)
        {
            await _notifications.ReplaceOneAsync(notification => notification.NotificationId == Guid.Parse(id), updatedNotification);
        }

    }
}
