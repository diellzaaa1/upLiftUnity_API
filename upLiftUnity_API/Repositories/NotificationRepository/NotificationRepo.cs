using MongoDB.Driver;
using upLiftUnity_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace upLiftUnity_API.Repositories
{
    public class NotificationRepo
    {
        private readonly IMongoCollection<Notification> _notifications;

        public NotificationRepo(IMongoDatabase database)
        {
            _notifications = database.GetCollection<Notification>("Notifications");
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _notifications.Find(_ => true).ToListAsync();
        }
 
        public async Task CreateAsync(Notification notification)
        {
            await _notifications.InsertOneAsync(notification);
        }

        public async Task UpdateIsReadAsync(Guid notificationId, bool isRead)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.NotificationId, notificationId);
            var update = Builders<Notification>.Update.Set(n => n.IsRead, isRead);
            await _notifications.UpdateOneAsync(filter, update);
        }
    }
}
