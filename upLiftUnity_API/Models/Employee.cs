namespace upLiftUnity_API.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public bool isActive { get; set; } = true;

        public virtual ICollection<Contract> Contracts { get; set;}
    }
}
