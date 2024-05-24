
using System.Collections.Concurrent;

using upLiftUnity_API.RealTimeChat.Model;
using upLiftUnity_API.RealTimeChat.Repository.MessageRepository;


namespace upLiftUnity_API.RealTimeChat.Services
{
    public class MessageBufferService : BackgroundService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ConcurrentQueue<Message> _messageBuffer;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(5);

        public MessageBufferService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            _messageBuffer = new ConcurrentQueue<Message>();
        }

        public void AddMessageToBuffer(Message message)
        {
            _messageBuffer.Enqueue(message);
            Console.WriteLine($"Message added to buffer: {message.Content}");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Message buffer service started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Executing SaveMessagesToDatabase...");
                await SaveMessagesToDatabase();
                Console.WriteLine("SaveMessagesToDatabase executed.");
                await Task.Delay(_interval, stoppingToken);
            }
        }


        private async Task SaveMessagesToDatabase()
        {
            Console.WriteLine("Saving messages to database...");

            var messagesToSave = new List<Message>();

            while (_messageBuffer.TryDequeue(out var message))
            {
                messagesToSave.Add(message);
            }

            Console.WriteLine($"Found {messagesToSave.Count} messages to save.");

            if (messagesToSave.Count > 0)
            {
                foreach (var message in messagesToSave)
                {
                    try
                    {
                        await _messageRepository.SaveMessageAsync(message);
                        Console.WriteLine($"Message saved: {message.Content}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving message: {ex.Message}");
                    }
                }
            }
        }

    }
}