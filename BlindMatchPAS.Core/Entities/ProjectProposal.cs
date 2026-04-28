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

    public class ProjectProposal
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Abstract { get; set; } = string.Empty;
        public string TechnicalStack { get; set; } = string.Empty;
        public int ResearchAreaId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; } = ProjectStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ResearchArea? ResearchArea { get; set; }
        public virtual ApplicationUser? Student { get; set; }
        public virtual Match? Match { get; set; }
    }
}
