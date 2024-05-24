using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using upLiftUnity_API.RealTimeChat.Model;
using upLiftUnity_API.RealTimeChat.Repositories;
using upLiftUnity_API.RealTimeChat.Repository.MessageRepository;

namespace upLiftUnity_API.RealTimeChat.Services
{
    public class MessageBufferService : IHostedService, IDisposable
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ConcurrentQueue<Message> _messageBuffer;
        private Timer _timer;

        public MessageBufferService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            _messageBuffer = new ConcurrentQueue<Message>();
        }

        public void AddMessageToBuffer(Message message)
        {
            _messageBuffer.Enqueue(message);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SaveMessagesToDatabase, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        private async void SaveMessagesToDatabase(object state)
        {
            var messagesToSave = new List<Message>();

            while (_messageBuffer.TryDequeue(out var message))
            {
                Console.WriteLine(message);
                messagesToSave.Add(message);
            }

            if (messagesToSave.Count > 0)
            {
                foreach (var message in messagesToSave)
                {
                    await _messageRepository.SaveMessageAsync(message);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
