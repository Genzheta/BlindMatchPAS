using System;

namespace BlindMatchPAS.Core.Entities
{
    // Represents a successful match between a student's proposal and a supervisor
    public class Match
    {
        // Primary key for the match record
        public int Id { get; set; }
        
        // The ID of the project proposal that was matched
        public int ProjectProposalId { get; set; }
        
        // The ID of the student who owns the proposal
        public string StudentId { get; set; } = string.Empty;
        
        // The ID of the supervisor who accepted the proposal
        public string SupervisorId { get; set; } = string.Empty;
        
        // The timestamp of when the match was confirmed
        public DateTime MatchedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties for easy access to related data
        public virtual ProjectProposal? ProjectProposal { get; set; }
        public virtual ApplicationUser? Student { get; set; }
        public virtual ApplicationUser? Supervisor { get; set; }
    }
}
