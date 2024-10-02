namespace upLiftUnity_API.Models
{
    public class Renovationn
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int BuildingId { get; set; }
        public Buildingg Buildingg { get; set; }
    }
}
