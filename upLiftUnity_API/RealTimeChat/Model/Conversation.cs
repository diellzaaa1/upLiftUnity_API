using System.ComponentModel.DataAnnotations;

namespace upLiftUnity_API.RealTimeChat.Model
{
    public class Conversation
    {
        [Key]
        public int ConversationId { get; set; }

        public string SenderEmail { get; set; }

        public string ReciverEmail { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
