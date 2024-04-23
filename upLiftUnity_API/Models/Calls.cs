using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace upLiftUnity_API.Models
{
    public class Calls
    {
        [Key]
        public int CallId { get; set; }
        public string CallerNickname { get; set; }

        public string Risk_Level { get; set; }

        [ForeignKey("UserId")]
        public int UserId {  get; set; }

        public User User { get; }


    }
}
