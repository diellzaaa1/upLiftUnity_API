using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace upLiftUnity_API.RealTimeChat.Model
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("ConversationId")]
        public int ConversationId { get; set; }

        public Conversation? Conversation { get; }

    }
}
