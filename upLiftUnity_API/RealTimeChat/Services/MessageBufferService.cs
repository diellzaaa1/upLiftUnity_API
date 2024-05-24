using System.Collections.Concurrent;
using upLiftUnity_API.RealTimeChat.Model;
using upLiftUnity_API.RealTimeChat.Repository.MessageRepository;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace upLiftUnity_API.RealTimeChat.Services
{
    public class MessageBufferService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentQueue<Message> _messageBuffer;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(5);
        private Task _processingTask;
        private CancellationTokenSource _cancellationTokenSource;

        public MessageBufferService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _messageBuffer = new ConcurrentQueue<Message>();
        }

        public void AddMessageToBuffer(Message message)
        {
            _messageBuffer.Enqueue(message);
            Console.WriteLine($"Message added to buffer: {message.Content}");
        }

        public void StartProcessing()
        {
            if (_processingTask != null && !_processingTask.IsCompleted)
            {
                Console.WriteLine("Message processing is already running.");
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            _processingTask = Task.Run(() => ProcessMessagesAsync(_cancellationTokenSource.Token));
            Console.WriteLine("Message buffer service started.");
        }

        public void StopProcessing()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                Console.WriteLine("Message buffer service stopping...");
            }
        }

        private async Task ProcessMessagesAsync(CancellationToken stoppingToken)
        {
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
                using (var scope = _serviceProvider.CreateScope())
                {
                    var messageRepository = scope.ServiceProvider.GetRequiredService<IMessageRepository>();

                    foreach (var message in messagesToSave)
                    {
                        try
                        {
                            await messageRepository.SaveMessageAsync(message);
                            Console.WriteLine($"Message saved: {message.Content}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error saving message: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No messages found in the buffer.");
            }
        }
    }
}
