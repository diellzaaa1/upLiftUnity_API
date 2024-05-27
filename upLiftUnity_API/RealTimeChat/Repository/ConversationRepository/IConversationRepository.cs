using System.Threading.Tasks;
using upLiftUnity_API.RealTimeChat.Model;

namespace upLiftUnity_API.RealTimeChat.Repositories
{
    public interface IConversationRepository
    {
        Task SaveConversationAsync(Conversation conversation);
        Task<Conversation> GetConversationByIdAsync(int conversationId);
        Task<IEnumerable<Conversation>> GetConversationsByEmailAsync(string email);
        Task<Conversation> GetOrCreateConversationAsync(string senderEmail, string receiverEmail);

        Task<int> GetConversationIdByEmailsAsync(string senderEmail, string receiverEmail);


    }
}
