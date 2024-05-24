using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;
using upLiftUnity_API.RealTimeChat.Model;

namespace upLiftUnity_API.RealTimeChat.Repository.MessageRepository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly APIDbContext _context;

        public MessageRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task SaveMessageAsync(Message message)
        {
            _context.Message.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<Message> GetMessageByIdAsync(int messageId)
        {
            return await _context.Message.FindAsync(messageId);
        }

        public async Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(int conversationId)
        {
            return await _context.Message
                                 .Where(m => m.ConversationId == conversationId)
                                 .ToListAsync();
        }
    }
}
