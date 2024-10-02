namespace upLiftUnity_API.Models
{
    public class Buildingg
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Renovationn> Renovationns { get; set; }
    }
}
