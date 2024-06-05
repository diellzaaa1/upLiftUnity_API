using System.ComponentModel.DataAnnotations;

namespace upLiftUnity_API.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        public string Suggestion { get; set; }

        public int Rating { get; set; }
    }
}
