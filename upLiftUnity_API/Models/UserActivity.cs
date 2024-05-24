using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace upLiftUnity_API.Models
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public DateTime LoginTime { get; set; }

        [ForeignKey("Id")]
        public int UserId { get; set; }
        public User? User { get; }
    }
}

