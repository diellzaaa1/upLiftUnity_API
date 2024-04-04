using System.ComponentModel.DataAnnotations;

namespace upLiftUnity_API.Models
{
    public class SupVol_Applications
    {
        [Key]
        public int ApplicationId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }

        public string CV { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string MotivationLetter { get; set; }

        public string ApplicationStatus { get; set; }


        public SupVol_Applications()
        {
            ApplicationStatus = "Pending";
        }
    }
}
