using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using upLiftUnity_API.RealTimeChat.Model;
using upLiftUnity_API.RealTimeChat.Services;
using upLiftUnity_API.RealTimeChat.Repositories;

namespace upLiftUnity_API.RealTimeChat.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();
        private readonly MessageBufferService _messageBufferService;
        private readonly IConversationRepository _conversationRepository;

        public ChatHub(MessageBufferService messageBufferService, IConversationRepository conversationRepository)
        {
            _messageBufferService = messageBufferService;
            _conversationRepository = conversationRepository;
        }

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

                // Ensure the conversation exists
                var conversation = await _conversationRepository.GetOrCreateConversationAsync(sender, recipient);

                Console.WriteLine(conversation);

                // Buffer the message
                var bufferedMessage = new Message
                {
                    Content = message,
                    ConversationId = conversation.ConversationId,
                    CreatedAt = DateTime.Now
                };

                _messageBufferService.AddMessageToBuffer(bufferedMessage);
            }
        }
    }
}
