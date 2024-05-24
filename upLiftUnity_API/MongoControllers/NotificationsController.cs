using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.DTOs.NotificationDtos;
using upLiftUnity_API.Models;
using upLiftUnity_API.Services;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace upLiftUnity_API.Controllers
{
    [Route("/notifications")]
    [ApiController]
    
    public class NotificationsController : ControllerBase
    {
        private readonly NotificationServ _notificationService;

        public NotificationsController(NotificationServ notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationDto notificationDto)
        {
            var notification = new Notification
            {
                Title = notificationDto.Title,
                Text = notificationDto.Text,
                IsRead = notificationDto.IsRead,
                CreatedOnUtc = notificationDto.CreatedOnUtc
            };

            await _notificationService.CreateNotificationAsync(notification);
            return Ok(notification);
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateNotification(string id, [FromBody] NotificationDto notificationDto)
        {
            try
            {
                var existingNotification = await _notificationService.GetNotificationByIdAsync(id);

                if (existingNotification == null)
                {
                    return NotFound();
                }
                existingNotification.IsRead = notificationDto.IsRead;

                await _notificationService.UpdateNotificationAsync(id, existingNotification);

                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }

}
