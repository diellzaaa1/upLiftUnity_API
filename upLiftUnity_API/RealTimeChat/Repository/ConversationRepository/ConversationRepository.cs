using System.Threading.Tasks;
using upLiftUnity_API.RealTimeChat.Model;
using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;

namespace upLiftUnity_API.RealTimeChat.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly APIDbContext _context;

        public ConversationRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task SaveConversationAsync(Model.Conversation conversation)
        {
            _context.Conversation.Add(conversation);
            await _context.SaveChangesAsync();
        }
        public async Task<Model.Conversation> GetConversationByIdAsync(int conversationId)
        {
            return await _context.Conversation.FindAsync(conversationId);
        }

        public async Task<IEnumerable<Model.Conversation>> GetConversationsByEmailAsync(string email)
        {
            return await _context.Conversation
                                 .Where(c => c.SenderEmail == email || c.ReciverEmail == email)
                                 .ToListAsync();
        }

        public async Task<Model.Conversation> GetOrCreateConversationAsync(string senderEmail, string receiverEmail)
        {
            var conversation = await _context.Conversation
                                             .FirstOrDefaultAsync(c => (c.SenderEmail == senderEmail && c.ReciverEmail == receiverEmail) ||
                                                                        (c.SenderEmail == receiverEmail && c.ReciverEmail == senderEmail));

            if (conversation == null)
            {
                conversation = new Model.Conversation
                {
                    SenderEmail = senderEmail,
                    ReciverEmail = receiverEmail,
                    CreatedAt = DateTime.Now
                };

                await SaveConversationAsync(conversation);
            }

            return conversation;
        }

        public async Task<int> GetConversationIdByEmailsAsync(string senderEmail, string receiverEmail)
        {
            var conversation = await _context.Conversation
                .FirstOrDefaultAsync(c =>
                    (c.SenderEmail == senderEmail && c.ReciverEmail == receiverEmail) ||
                    (c.SenderEmail == receiverEmail && c.ReciverEmail == senderEmail)
                );

            if (conversation == null)
            {
                // Kthe -1 nëse nuk ka konversation
                return -1;
            }

            return conversation.ConversationId;
        }

    }


}
