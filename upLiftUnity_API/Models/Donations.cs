using System.ComponentModel.DataAnnotations;

namespace upLiftUnity_API.Models
{
    public class Donations
    {
        [Key]
        public int DonationID {  get; set; }

        public string NameSurname {  get; set; }

        public string Email {  get; set; }

        public string Address { get; set; }

        public int Amount {  get; set; }

        public string TransactionId { get; set; } 

        public DateTime Date{ get; set; }

        

    }
}
