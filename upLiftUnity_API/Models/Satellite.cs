namespace upLiftUnity_API.Models
{
    public class Satellite
    {
        public int SatelliteId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int PlanetId { get; set; }
        public Planet? Planet { get; set; }
    }
}
