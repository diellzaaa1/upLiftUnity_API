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
                                 .Where(c => c.User1 == email || c.User2 == email)
                                 .ToListAsync();
        }

        public async Task<Model.Conversation> GetOrCreateConversationAsync(string user1, string user2)
        {
            var conversation = await _context.Conversation
                                             .FirstOrDefaultAsync(c => (c.User1 == user1 && c.User2 == user2) ||
                                                                        (c.User1 == user2 && c.User2 == user1));

            if (conversation == null)
            {
                conversation = new Model.Conversation
                {
                    User1 = user1,
                    User2 = user2,
                    CreatedAt = DateTime.Now
                };

                await SaveConversationAsync(conversation);
            }

            return conversation;
        }

        public async Task<int> GetConversationIdByEmailsAsync(string user1, string user2)
        {
            var conversation = await _context.Conversation
                .FirstOrDefaultAsync(c =>
                    (c.User1 == user1 && c.User2 == user2) ||
                    (c.User1 == user2 && c.User2 == user1)
                );

            if (conversation == null)
            {
                return -1;
            }

            return conversation.ConversationId;
        }

    }


}
