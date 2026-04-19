namespace BlindMatchPAS.Core.Entities
{
    // Represents a research category (e.g., AI, Cybersecurity)
    public class ResearchArea
    {
        // Primary key for the research area
        public int Id { get; set; }
        
        // Name of the research area
        public string Name { get; set; } = string.Empty;
        
        // Detailed description of what the area covers
        public string? Description { get; set; }

        // Navigation property: All project proposals belonging to this area
        public virtual ICollection<ProjectProposal> Proposals { get; set; } = new List<ProjectProposal>();
        
        // Navigation property: All supervisors who have expertise in this area
        public virtual ICollection<SupervisorExpertise> SupervisorExpertises { get; set; } = new List<SupervisorExpertise>();
    }
}

