using upLiftUnity_API.RealTimeChat.Model;

namespace upLiftUnity_API.RealTimeChat.Repository.MessageRepository
{
    public interface IMessageRepository
    {
        Task SaveMessageAsync(Message message);
        Task<Message> GetMessageByIdAsync(int messageId);
        Task<IEnumerable<Message>> GetMessagesByConversationIdAsync(int conversationId);

    }
}
