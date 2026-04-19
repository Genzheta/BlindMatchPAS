namespace BlindMatchPAS.Core.Entities
{
    // Possible states of a research project proposal
    public enum ProjectStatus
    {
        Pending,        // Just created, waiting for review
        UnderReview,    // A supervisor is currently looking at it
        Matched,        // Successfully matched with a supervisor
        Withdrawn       // Cancelled by the student
    }

    // Represents a research project proposal submitted by a student
    public class ProjectProposal
    {
        // Primary key for the proposal
        public int Id { get; set; }
        
        // Title of the research project
        public string Title { get; set; } = string.Empty;
        
        // Brief summary of the research goals
        public string Abstract { get; set; } = string.Empty;
        
        // Tools and technologies to be used (e.g., Python, React)
        public string TechnicalStack { get; set; } = string.Empty;
        
        // Link to the research area category
        public int ResearchAreaId { get; set; }
        
        // ID of the student who submitted the proposal
        public string StudentId { get; set; } = string.Empty;
        
        // Current status of the proposal
        public ProjectStatus Status { get; set; } = ProjectStatus.Pending;
        
        // Date and time when the proposal was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ResearchArea? ResearchArea { get; set; }
        public virtual ApplicationUser? Student { get; set; }
        public virtual Match? Match { get; set; }
    }
}
