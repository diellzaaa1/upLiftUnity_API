namespace upLiftUnity_API.Models
{
    public class Planet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Satelite> Satelites { get; set; }
    }
}
