using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Web.Models.Supervisor
{
    public class SupervisorDashboardViewModel
    {
        public List<Match> MatchedProjects { get; set; } = new List<Match>();
        public List<ResearchArea> PreferredResearchAreas { get; set; } = new List<ResearchArea>();
    }
}
