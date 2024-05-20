using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using upLiftUnity_API.DTOs.NotificationDtos;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace upLiftUnity_API.Repositories.NotificationRepository
{
    public class NotificationHub : Hub<IClientNotificationHub>
    {
        private readonly ILogger<NotificationHub> _logger;
       
        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger; 
        }
        private const string NotificationsGroup = "NotificationGroup";
        public async Task JoinNotificationGroup(int userId)
        {
            await Groups.AddToGroupAsync(NotificationsGroup, userId.ToString());
        }


        public async Task RegisterForWebNotifications(int userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());
            await Clients.Groups(userId.ToString()).SendWelcomeMessageToNewClients("Welcome to donations web!");
        }

        public async Task DeregisterFromWebNotifications(int userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId.ToString());
        }

        public async Task SendNotificationToClient(int userId, NotificationDto notification)
        {
            var clients = Clients;
            await Clients.Groups(userId.ToString()).SendNotificationToClient(notification);
            Console.WriteLine($"Notification sent to user {userId}: {notification.Text}");
            _logger.LogInformation($"Notification sent to user {userId}: {notification.Text}");
        }
    }
}
