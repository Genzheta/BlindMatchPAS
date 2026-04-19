using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Core.Interfaces
{
    public interface IMatchService
    {
        Task<bool> MatchProposalAsync(int proposalId, string supervisorId);
        Task<List<ProjectProposal>> GetMatchedProjectsForSupervisorAsync(string supervisorId);
        Task<Match?> GetMatchDetailsAsync(int proposalId);
        Task<bool> ExpressInterestAsync(int proposalId);
    }
}
