using BlindMatchPAS.Core.Entities;
using BlindMatchPAS.Core.Interfaces;
using BlindMatchPAS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlindMatchPAS.Infrastructure.Services
{
    // Implementation of IMatchService to handle matching logic
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _context;

        // Constructor: Injects the database context
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
            // Placeholder logic for expressing interest
            // In a real app, this might create a notification or a record in an Interest table
            return await Task.FromResult(true);
        }

        // Confirms a match between a proposal and a supervisor
        public async Task<bool> MatchProposalAsync(int proposalId, string supervisorId)
        {
            // Find the proposal in the database
            var proposal = await _context.ProjectProposals.FindAsync(proposalId);
            if (proposal == null || proposal.Status == ProjectStatus.Matched)
            {
                return false; // Cannot match if already matched or not found
            }

            // Create a new Match record
            var match = new Match
            {
                ProjectProposalId = proposalId,
                StudentId = proposal.StudentId,
                SupervisorId = supervisorId,
                MatchedAt = DateTime.UtcNow
            };

            // Update proposal status to Matched
            proposal.Status = ProjectStatus.Matched;
            _context.Matches.Add(match);
            
            // Save everything to the database
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
