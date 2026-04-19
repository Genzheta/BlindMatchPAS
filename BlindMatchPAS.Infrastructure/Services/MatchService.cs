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

        public async Task<bool> MatchProposalAsync(int proposalId, string supervisorId)
        {
            var proposal = await _context.ProjectProposals.FindAsync(proposalId);

            if (proposal == null || proposal.Status == ProjectStatus.Matched)
            {
                return false;
            }

            var match = new Match
            {
                ProjectProposalId = proposal.Id,
                StudentId = proposal.StudentId,
                SupervisorId = supervisorId,
                MatchedAt = DateTime.UtcNow,
                IsRevealed = true // Handled automatically for now
            };

            proposal.Status = ProjectStatus.Matched;
            _context.Matches.Add(match);
            
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<ProjectProposal>> GetMatchedProjectsForSupervisorAsync(string supervisorId)
        {
            return await _context.ProjectProposals
                .AsNoTracking()
                .Include(p => p.ResearchArea)
                .Include(p => p.Match)
                    .ThenInclude(m => m!.Student)
                .Where(p => p.Match != null && p.Match.SupervisorId == supervisorId)
                .ToListAsync();
        }

        public async Task<Match?> GetMatchDetailsAsync(int proposalId)
        {
            return await _context.Matches
                .AsNoTracking()
                .Include(m => m.Supervisor)
                .Include(m => m.Student)
                .FirstOrDefaultAsync(m => m.ProjectProposalId == proposalId);
        }

        public async Task<bool> ExpressInterestAsync(int proposalId)
        {
            var proposal = await _context.ProjectProposals.FindAsync(proposalId);
            if (proposal != null && proposal.Status == ProjectStatus.Pending)
            {
                proposal.Status = ProjectStatus.UnderReview;
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
