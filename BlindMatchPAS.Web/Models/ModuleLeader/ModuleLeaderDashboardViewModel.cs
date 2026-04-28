using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Web.Models.ModuleLeader
{
    public class ModuleLeaderDashboardViewModel
    {
        public int TotalProposals { get; set; }
        public int PendingProposals { get; set; }
        public int MatchedProposals { get; set; }
        public List<ProjectProposal> RecentProposals { get; set; } = new List<ProjectProposal>();
    }
}
