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

        public int UserId {  get; set; }

        [ForeignKey("UserId")]
        public User User{ get; set; }


    }
}
