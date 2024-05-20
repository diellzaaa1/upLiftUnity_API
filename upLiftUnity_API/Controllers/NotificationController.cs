using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks;
using upLiftUnity_API.DTOs.NotificationDtos;
using upLiftUnity_API.Models;
using upLiftUnity_API.Repositories.NotificationRepository;
using IClientNotificationHub = upLiftUnity_API.Repositories.NotificationRepository.IClientNotificationHub;


namespace upLiftUnity_API.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub, IClientNotificationHub> _hubContext;

        public NotificationController(IHubContext<NotificationHub, IClientNotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("userupdate")]
        public async Task<IActionResult> UserUpdateAsync(User user)
        {
            try
            {
                var notification = new NotificationDto()
                {
                    UserId = user.Id,
                    IsAcknowledged = false,
                    RelativeNotifiedDateAndTime = GetRelativeNotifiedDateAndTime(DateTime.Now),
                    Title = "Client Updated.",
                    Text = "Client details have been updated successfully by the system admin.",
                    NotificationEvent = "success"
                };

                // Send notification to the specified user group
                await _hubContext.Clients.Groups(user.Id.ToString()).SendNotificationToClient(notification);

                return Ok("Notification sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("Notifications")]
        public List<NotificationDto> GetUserNotifications([FromQuery] int userId)
        {
            return GetUserDummyNotifications().FindAll(i => i.UserId == userId);
        }
        private List<NotificationDto> GetUserDummyNotifications()
        {
            return new List<NotificationDto>()
    {
        new NotificationDto()
        {
            Title = "Username Updated Successfully.",
            Text = "Username has been chanegd to username@localhost.com",
            RelativeNotifiedDateAndTime = GetRelativeNotifiedDateAndTime(DateTime.Now.AddMinutes(-2)),
            NotificationEvent = "success",
            UserId = 2,
            Id = 1
        },
        new NotificationDto()
        {
            Title = "Application Rejected.",
            Text = "Your application has been rejected. Please contact Customer Contact Center at 081-2222-5674",
            RelativeNotifiedDateAndTime = GetRelativeNotifiedDateAndTime(DateTime.Now.AddDays(-1)),
            NotificationEvent = "danger",
            UserId = 2,
            Id = 2
        },
        new NotificationDto()
        {
            Title = "Application Submitted Successfully.",
            Text = "Your application has been submitted for the approval.",
            RelativeNotifiedDateAndTime = GetRelativeNotifiedDateAndTime(DateTime.Now.AddDays(-3)),
            NotificationEvent = "info",
            UserId = 2,
            Id = 3
        },
    };
        }

        private string GetRelativeNotifiedDateAndTime(DateTime notifiedDateAndTime)
        {
            Dictionary<long, string> thresholds = new Dictionary<long, string>();
            int minute = 60;
            int hour = 60 * minute;
            int day = 24 * hour;

            thresholds.Add(60, "{0} seconds ago");
            thresholds.Add(minute * 2, "a minute ago");
            thresholds.Add(45 * minute, "{0} minutes ago");
            thresholds.Add(120 * minute, "an hour ago");
            thresholds.Add(day, "{0} hours ago");
            thresholds.Add(day * 2, "yesterday");
            thresholds.Add(day * 30, "{0} days ago");
            thresholds.Add(day * 365, "{0} months ago");
            thresholds.Add(long.MaxValue, "{0} years ago");
       
            long since = (DateTime.Now.Ticks - notifiedDateAndTime.Ticks) / 10000000;

            foreach (long threshold in thresholds.Keys)
            {
                if (since < threshold)
                {
                    TimeSpan t = new TimeSpan(DateTime.Now.Ticks - notifiedDateAndTime.Ticks);
                    return string.Format(thresholds[threshold], GetTimeValueForThreshold(t));
                }
            }

            return string.Empty;
        }

        private int GetTimeValueForThreshold(TimeSpan timeSpan)
        {
            if (timeSpan.Days >= 365)
                return timeSpan.Days / 365;
            else if (timeSpan.Days >= 30)
                return timeSpan.Days / 30;
            else if (timeSpan.Days >= 2)
                return timeSpan.Days;
            else if (timeSpan.Hours > 0)
                return timeSpan.Hours;
            else if (timeSpan.Minutes > 0)
                return timeSpan.Minutes;
            else
                return timeSpan.Seconds;
        }
    }
}
