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

      
    }

}
