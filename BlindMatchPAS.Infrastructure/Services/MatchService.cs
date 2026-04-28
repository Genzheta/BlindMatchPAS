using BlindMatchPAS.Core.Entities;
using BlindMatchPAS.Core.Interfaces;
using BlindMatchPAS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlindMatchPAS.Infrastructure.Services
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _context;

        public MatchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Match>> GetMatchedProjectsForSupervisorAsync(string supervisorId)
        {
            return await _context.Matches
                .Include(m => m.ProjectProposal)
                .Include(m => m.Student)
                .Where(m => m.SupervisorId == supervisorId)
                .ToListAsync();
        }

        public async Task<bool> ExpressInterestAsync(int proposalId)
        {
            return await Task.FromResult(true);
        }

        public async Task<bool> MatchProposalAsync(int proposalId, string supervisorId)
        {
            var proposal = await _context.ProjectProposals.FindAsync(proposalId);
            if (proposal == null || proposal.Status == ProjectStatus.Matched)
            {
                return false;
            }

            var match = new Match
            {
                ProjectProposalId = proposalId,
                StudentId = proposal.StudentId,
                SupervisorId = supervisorId,
                MatchedAt = DateTime.UtcNow
            };

            proposal.Status = ProjectStatus.Matched;
            _context.Matches.Add(match);
            
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
