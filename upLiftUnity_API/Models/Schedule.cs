using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace upLiftUnity_API.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime SecondDate { get; set; }
        public DateTime ThirdDate { get; set; }
        public DateTime FourthDate { get; set; }

       

        [ForeignKey("Id")]

        public int UserId { get; set; }

        public User? UserSch { get; }   



    }
}
