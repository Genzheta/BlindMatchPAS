using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Web.Models.Supervisor
{
    public class ExpertiseViewModel
    {
        public List<int>? SelectedAreaIds { get; set; }
        public List<ResearchArea>? AllResearchAreas { get; set; }
    }
}
