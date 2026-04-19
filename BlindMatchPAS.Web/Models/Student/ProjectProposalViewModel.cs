using System.ComponentModel.DataAnnotations;
using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Web.Models.Student
{
    public class ProjectProposalViewModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.MultilineText)]
        public string Abstract { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Technical Stack")]
        public string TechnicalStack { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Research Area")]
        public int ResearchAreaId { get; set; }

        public List<ResearchArea>? ResearchAreas { get; set; }
    }
}
