namespace upLiftUnity_API.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
