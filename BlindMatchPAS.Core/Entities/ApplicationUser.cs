using Microsoft.AspNetCore.Identity;

namespace BlindMatchPAS.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; 
        public string? Department { get; set; }

        public virtual ICollection<ProjectProposal> Proposals { get; set; } = new List<ProjectProposal>();
        public virtual ICollection<SupervisorExpertise> Expertises { get; set; } = new List<SupervisorExpertise>();
        public virtual ICollection<Match> MatchesAsStudent { get; set; } = new List<Match>();
        public virtual ICollection<Match> MatchesAsSupervisor { get; set; } = new List<Match>();
    }
}
