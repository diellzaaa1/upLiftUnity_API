using MongoDB.Driver;
using upLiftUnity_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using upLiftUnity_API.Repositories.NotificationRepository;
using upLiftUnity_API.MongoModels;

namespace upLiftUnity_API.Repositories
{
    public class NotificationRepo : INotificationRepository
    {
        private readonly IMongoCollection<Notification> _notifications;
        private readonly MongoDbContext _dbContext;

        public NotificationRepo(MongoDbContext context)
        {
            _dbContext = context;
            _notifications = context.Database.GetCollection<Notification>("notification");
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _notifications.Find(_ => true).ToListAsync();
        }
 
        public async Task CreateNotificationAsync(Notification notification)
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
