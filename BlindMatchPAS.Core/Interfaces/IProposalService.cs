using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Core.Interfaces
{
    public interface IProposalService
    {
        Task<List<ProjectProposal>> GetProposalsForStudentAsync(string studentId);
        Task<ProjectProposal?> GetProposalByIdAsync(int id);
        Task<ProjectProposal?> GetProposalByIdForStudentAsync(int id, string studentId);
        Task<bool> CreateProposalAsync(ProjectProposal proposal);
        Task<bool> UpdateProposalAsync(ProjectProposal proposal);
        Task<bool> WithdrawProposalAsync(int id, string studentId);
        Task<List<ProjectProposal>> GetAvailableProposalsForSupervisorAsync(int? areaId, List<int> preferredAreaIds, string? searchTerm = null);
        Task<List<ResearchArea>> GetResearchAreasAsync();
        Task<List<ResearchArea>> GetSupervisorExpertiseAsync(string supervisorId);
        Task<bool> UpdateSupervisorExpertiseAsync(string supervisorId, List<int> areaIds);
    }
}
