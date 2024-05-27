using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.DTOs.NotificationDtos;
using upLiftUnity_API.Models;
using upLiftUnity_API.Services;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq.Expressions;
using upLiftUnity_API.MongoModels;
using upLiftUnity_API.Repositories.UserRepository;
using Microsoft.AspNetCore.Authorization;

namespace upLiftUnity_API.Controllers
{
    [Route("/notifications")]
    [ApiController]
    
    public class NotificationsController : Controller
    {
 
        private readonly IMongoCollection<Notification> _notifications;
        private readonly IUserRepository _userRepository;


        public NotificationsController(MongoDbContext mongoDbContext, IUserRepository userRepository)
        {
            _notifications = mongoDbContext.Database?.GetCollection<Notification>("notification");
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] Notification notification)
        {
            try
            {
                notification.NotificationId = Guid.NewGuid();
                notification.Title = notification.Title;
                notification.Text = notification.Text;
                notification.CreatedOnUtc = DateTime.UtcNow;
                notification.IsRead = notification.IsRead;

                await _notifications.InsertOneAsync(notification);
                return Ok(notification);
            }

            catch (Exception ex)

            {
                return StatusCode(500, $"An error occurred:{ex.Message}");
            }
        }

        [HttpPost("updateReadStatus")]
        public async Task<IActionResult> UpdateReadStatus([FromBody] Notification updateReadStatusDto)
        {
            try
            {
                var filter = Builders<Notification>.Filter.Eq(n => n.NotificationId, updateReadStatusDto.NotificationId);
                var update = Builders<Notification>.Update.Set(n => n.IsRead, updateReadStatusDto.IsRead);

                var result = await _notifications.UpdateOneAsync(filter, update);

                if (result.ModifiedCount == 1)
                {
                    return Ok();
                }
                else
                {
                    return NotFound("Notification not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("getNotifications")]
  
        public async Task<IActionResult> GetAllNotifications()
        {
            try
            {
                var notifications = await _notifications.Find(_ => true)
                                                .SortByDescending(n => n.CreatedOnUtc)
                                                .ToListAsync();

                var notificationDtos = notifications.Select(n => new NotificationDto
                {
                    Title = n.Title,
                    Text = n.Text,
                    CreatedOnUtc = n.CreatedOnUtc,
                }).ToList();

                return Ok(notificationDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }

}
