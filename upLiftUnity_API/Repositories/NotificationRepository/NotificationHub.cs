using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using upLiftUnity_API.DTOs.NotificationDtos;

namespace upLiftUnity_API.Repositories.NotificationRepository
{
    public class NotificationHub : Hub<IClientNotificationHub>
    {
        private const string NotificationsGroup = "NotificationGroup";
        public NotificationHub()
        {

        }
        public async Task JoinNotificationGroup(int userId)
        {
            await Groups.AddToGroupAsync(NotificationsGroup, userId.ToString());
        }


        public async Task RegisterForWebNotifications(int userId)
        {
            await Groups.AddToGroupAsync(NotificationsGroup, userId.ToString());
        }

        public async Task DeregisterFromWebNotifications(int userId)
        {
            await Groups.RemoveFromGroupAsync(NotificationsGroup, userId.ToString());
        }

        public async Task SendNotificationToClient(int userId, NotificationDto notification)
        {
            // Send notification to a specific client (user) based on userId
            await Clients.Groups(userId.ToString()).SendNotificationToClient(notification);
        }
    }
}
