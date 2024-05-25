using System.ComponentModel.DataAnnotations;

namespace upLiftUnity_API.RealTimeChat.Model
{
    public class Conversation
    {
        [Key]
        public int ConversationId { get; set; }

        public string User1 { get; set; }

        public string User2 { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
