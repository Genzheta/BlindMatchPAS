using Microsoft.AspNetCore.Identity;

namespace BlindMatchPAS.Core.Entities
{
    // Extends the default Identity user to include custom profile information
    public class ApplicationUser : IdentityUser
    {
        // User's full display name
        public string FullName { get; set; } = string.Empty;
        
        // System role: Student, Supervisor, ModuleLeader, or Admin
        public string Role { get; set; } = string.Empty; 
        
        // Academic department or faculty
        public string? Department { get; set; }

        // Navigation properties
        // Proposals created by this user (if they are a student)
        public virtual ICollection<ProjectProposal> Proposals { get; set; } = new List<ProjectProposal>();
        
        // Areas of research expertise (if they are a supervisor)
        public virtual ICollection<SupervisorExpertise> Expertises { get; set; } = new List<SupervisorExpertise>();
        
        // Matches where this user is the student
        public virtual ICollection<Match> MatchesAsStudent { get; set; } = new List<Match>();
        
        // Matches where this user is the supervisor
        public virtual ICollection<Match> MatchesAsSupervisor { get; set; } = new List<Match>();
    }
}
