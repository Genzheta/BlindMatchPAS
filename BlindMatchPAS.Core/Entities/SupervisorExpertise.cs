namespace BlindMatchPAS.Core.Entities
{
    // Represents a many-to-many relationship between Supervisors and Research Areas
    public class SupervisorExpertise
    {
        // The ID of the supervisor
        public string SupervisorId { get; set; } = string.Empty;
        
        // The ID of the research area they are expert in
        public int ResearchAreaId { get; set; }

        // Navigation properties for easy access to related data
        public virtual ApplicationUser? Supervisor { get; set; }
        public virtual ResearchArea? ResearchArea { get; set; }
    }
}
