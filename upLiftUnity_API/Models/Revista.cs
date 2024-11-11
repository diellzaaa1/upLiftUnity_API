
namespace upLiftUnity_API.Models
{
    public class Revista
    {
        public int Id { get; set; }
        public string MagazineName { get; set; }
        public int IssueNumber { get; set; }
        public int PublisherId { get; set; }
        public Botuesi Publisher { get; set; }

    }
}
