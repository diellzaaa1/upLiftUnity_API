using Microsoft.AspNetCore.SignalR;
//using upLiftUnity_API.DTOs.NotificationDtos;
using upLiftUnity_API.Repositories.NotificationRepository;

namespace upLiftUnity_API.Services
{
    public class NotificationService
    {
        private readonly IHubContext<NotificationHub, IClientNotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub, IClientNotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }
    }

  
}
