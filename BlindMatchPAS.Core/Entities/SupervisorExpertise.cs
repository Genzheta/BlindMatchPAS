namespace BlindMatchPAS.Core.Entities
{
    public class SupervisorExpertise
    {
        public string SupervisorId { get; set; } = string.Empty;
        public int ResearchAreaId { get; set; }

        // Navigation properties
        public virtual ApplicationUser? Supervisor { get; set; }
        public virtual ResearchArea? ResearchArea { get; set; }
    }
}
