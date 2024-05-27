using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using upLiftUnity_API.DTOs.NotificationDtos;
using upLiftUnity_API.Repositories.NotificationRepository;

namespace upLiftUnity_API.Repositories.NotificationRepository
{
    public class AdminNotificationScheduler
    {
        private readonly ILogger<AdminNotificationScheduler> _logger;
        private readonly IHubContext<NotificationHub, IClientNotificationHub> _hubContext;

        public AdminNotificationScheduler(ILogger<AdminNotificationScheduler> logger, IHubContext<NotificationHub, IClientNotificationHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
            ScheduleAdminNotifications();
        }

        private void ScheduleAdminNotifications()
        {
            DateTime nextNotificationDate = GetNextMonthFirstDay();
            TimeSpan delay = nextNotificationDate - DateTime.Now;

            Task.Delay(delay).ContinueWith(_ =>
            {
                SendAdminNotification();
                ScheduleAdminNotifications();
            });
        }

        private DateTime GetNextMonthFirstDay()
        {
            DateTime today = DateTime.Today;
            DateTime firstDayOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);
            return firstDayOfNextMonth;
        }

        private async void SendAdminNotification()
        {
            try
            {
                int adminUserId = 3; 
                NotificationDto notification = new NotificationDto
                {
                    Title = "Admin Notification",
                    Text = "YOU HAVE TO REGISTER THE SCHEDULE FOR THIS MONTH",
                    CreatedOnUtc = DateTime.UtcNow
                };

                await _hubContext.Clients.Groups($"Role_Admin").SendNotificationToClient(notification);
                _logger.LogInformation($"Admin notification sent: {notification.Text}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending admin notification: {ex.Message}");
            }
        }
    }
}
