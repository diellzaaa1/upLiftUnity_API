using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using upLiftUnity_API.DTOs.NotificationDtos;

namespace upLiftUnity_API.Repositories.NotificationRepository
{
    public interface IClientNotificationHub
    {
        Task ClientReceiveMessage(string user, string message);
        Task SendNotificationToClient(NotificationDto notification);
    }
}
