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
       
        public string Description { get; set; }

        [DataType(DataType.Upload)]

        public string CV {  get; set; }


        public string ApplicationStatus { get; set; } = "e pa shqyrtuar";

        public string ApplicationType { get; set; }


    }
}
