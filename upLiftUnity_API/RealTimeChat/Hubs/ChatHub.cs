using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace upLiftUnity_API.RealTimeChat.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            var email = Context.GetHttpContext().Request.Query["email"];

            if (!string.IsNullOrEmpty(email))
            {
                Users[email] = Context.ConnectionId;
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var email = Context.GetHttpContext().Request.Query["email"];

            if (!string.IsNullOrEmpty(email))
            {
                Users.TryRemove(email, out _);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendToSpecific(string sender, string message, string recipient)
        {
            if (Users.TryGetValue(recipient, out string connectionId))
            {
                await Clients.Client(connectionId).SendAsync("broadcastMessage", sender, message);
            }
        }
    }
    }
