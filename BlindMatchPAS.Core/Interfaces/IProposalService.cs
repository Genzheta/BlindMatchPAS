using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Core.Interfaces
{
    // Interface for managing project proposals and research expertise
    public interface IProposalService
    {
        // Gets all proposals created by a specific student
        Task<List<ProjectProposal>> GetProposalsForStudentAsync(string studentId);
        
        // Gets details of a specific proposal by its ID
        Task<ProjectProposal?> GetProposalByIdAsync(int id);
        
        // Gets a specific proposal for a student, ensuring they own it
        Task<ProjectProposal?> GetProposalByIdForStudentAsync(int id, string studentId);
        
        // Submits a new project proposal to the system
        Task<bool> CreateProposalAsync(ProjectProposal proposal);
        
        // Updates the details of an existing proposal
        Task<bool> UpdateProposalAsync(ProjectProposal proposal);
        
        // Cancels/withdraws a proposal from consideration
        Task<bool> WithdrawProposalAsync(int id, string studentId);
        
        // Gets proposals that are available for a supervisor to review
        Task<List<ProjectProposal>> GetAvailableProposalsForSupervisorAsync(int? areaId, List<int> preferredAreaIds, string? searchTerm = null);
        
        // Gets a list of all research areas in the system
        Task<List<ResearchArea>> GetResearchAreasAsync();
        
        // Gets the research areas a supervisor is expert in
        Task<List<ResearchArea>> GetSupervisorExpertiseAsync(string supervisorId);
        
        // Updates a supervisor's list of expertise areas
        Task<bool> UpdateSupervisorExpertiseAsync(string supervisorId, List<int> areaIds);
    }
}
