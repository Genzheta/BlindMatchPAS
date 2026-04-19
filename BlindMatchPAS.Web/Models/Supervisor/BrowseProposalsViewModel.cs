using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Web.Models.Supervisor
{
    public class BrowseProposalsViewModel
    {
        public List<ProjectProposal> AvailableProposals { get; set; } = new List<ProjectProposal>();
        public List<ResearchArea> AllResearchAreas { get; set; } = new List<ResearchArea>();
        public int? FilteredAreaId { get; set; }
        public string? SearchTerm { get; set; }
    }
}
