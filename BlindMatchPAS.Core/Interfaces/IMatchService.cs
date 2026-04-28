using BlindMatchPAS.Core.Entities;

namespace BlindMatchPAS.Core.Interfaces
{
    public interface IMatchService
    {
        Task<List<Match>> GetMatchedProjectsForSupervisorAsync(string supervisorId);
        Task<bool> ExpressInterestAsync(int proposalId);
        Task<bool> MatchProposalAsync(int proposalId, string supervisorId);
    }
}
