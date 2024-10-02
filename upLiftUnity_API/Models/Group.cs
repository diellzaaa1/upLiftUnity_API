namespace upLiftUnity_API.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}
