using System;

namespace BlindMatchPAS.Core.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int ProjectProposalId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string SupervisorId { get; set; } = string.Empty;
        public DateTime MatchedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ProjectProposal? ProjectProposal { get; set; }
        public virtual ApplicationUser? Student { get; set; }
        public virtual ApplicationUser? Supervisor { get; set; }
    }
}
