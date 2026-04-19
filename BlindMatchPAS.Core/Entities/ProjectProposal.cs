namespace BlindMatchPAS.Core.Entities
{
    public enum ProjectStatus
    {
        Pending,
        UnderReview,
        Matched,
        Withdrawn
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
