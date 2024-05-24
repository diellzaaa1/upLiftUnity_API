using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using upLiftUnity_API.DTOs.NotificationDtos;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.Repositories.NotificationRepository
{
    public class NotificationHub : Hub<IClientNotificationHub>
    {
        private readonly ILogger<NotificationHub> _logger;
        private const string RoleGroupPrefix = "Role_";

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger; 
        }
        private const string NotificationsGroup = "NotificationGroup";
        public async Task JoinNotificationGroup(int userId)
        {
            await Groups.AddToGroupAsync(NotificationsGroup, userId.ToString());
        }


        public async Task RegisterForWebNotifications(int userId, string roleName)
        {
           await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());
           await Groups.AddToGroupAsync(Context.ConnectionId, $"{RoleGroupPrefix}{roleName}");
            await Clients.Groups(userId.ToString()).SendWelcomeMessageToNewClients("Welcome to donations web!");
        }

        public async Task DeregisterFromWebNotifications(int userId, string roleName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId.ToString());
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{RoleGroupPrefix}{roleName}");
        }

        public async Task SendNotificationToClient(string roleName, NotificationDto notification)
        {
            await Clients.Groups($"{RoleGroupPrefix}{roleName}").SendNotificationToClient(notification);
            _logger.LogInformation($"Notification sent to role {roleName}: {notification.Text}");
        }
    }
}
