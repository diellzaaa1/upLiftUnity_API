namespace upLiftUnity_API.Models
{
    public class Satelite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int PlanetId { get; set; }
        public Planet Planet { get; set; }
    }
}
