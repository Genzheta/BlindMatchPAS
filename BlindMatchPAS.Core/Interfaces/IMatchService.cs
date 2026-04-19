using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Core.Interfaces
{
    // Interface for managing the matching process between students and supervisors
    public interface IMatchService
    {
        // Gets a list of all proposals successfully matched to a specific supervisor
        Task<List<Match>> GetMatchedProjectsForSupervisorAsync(string supervisorId);
        
        // Allows a supervisor to express initial interest in a proposal
        Task<bool> ExpressInterestAsync(int proposalId);
        
        // Confirms a final match between a proposal and a supervisor
        Task<bool> MatchProposalAsync(int proposalId, string supervisorId);
    }
}
