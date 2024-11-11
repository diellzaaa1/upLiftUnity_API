namespace upLiftUnity_API.Models
{
    public class Botuesi
    {
        public int Id { get; set; }
        public string PublisherName { get; set; }
        public string Location { get; set; }
        public ICollection<Revista> Revistat { get; set; }
    }
}
