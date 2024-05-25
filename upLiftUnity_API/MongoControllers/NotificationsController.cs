using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.DTOs.NotificationDtos;
using upLiftUnity_API.Models;
using upLiftUnity_API.Services;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq.Expressions;
using upLiftUnity_API.MongoModels;

namespace upLiftUnity_API.Controllers
{
    [Route("/notifications")]
    [ApiController]
    
    public class NotificationsController : Controller
    {
 
        private readonly IMongoCollection<Notification> _notifications;
       

        public NotificationsController(MongoDbContext mongoDbContext)
        {
            _notifications = mongoDbContext.Database?.GetCollection<Notification>("notification");
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

        //[HttpPut("Update/{id}")]
        //public async Task<IActionResult> UpdateNotification(string id, [FromBody] NotificationDto notificationDto)
        //{
        //    try
        //    {
        //        var existingNotification = await _notificationService.GetNotificationByIdAsync(id);

        //        if (existingNotification == null)
        //        {
        //            return NotFound();
        //        }
        //       existingNotification.IsRead = notificationDto.IsRead;

        //        await _notificationService.UpdateNotificationAsync(id, existingNotification);

        //        return NoContent(); 
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

    }

}
