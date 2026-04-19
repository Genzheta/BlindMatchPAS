using BlindMatchPAS.Core.Entities;
using BlindMatchPAS.Core.Interfaces;
using BlindMatchPAS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlindMatchPAS.Infrastructure.Services
{
    public class ProposalService : IProposalService
    {
        private readonly ApplicationDbContext _context;

        public ProposalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectProposal>> GetProposalsForStudentAsync(string studentId)
        {
            return await _context.ProjectProposals
                .AsNoTracking()
                .Include(p => p.ResearchArea)
                .Where(p => p.StudentId == studentId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<ProjectProposal?> GetProposalByIdAsync(int id)
        {
            return await _context.ProjectProposals
                .Include(p => p.ResearchArea)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ProjectProposal?> GetProposalByIdForStudentAsync(int id, string studentId)
        {
            return await _context.ProjectProposals
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id && p.StudentId == studentId);
        }

        public async Task<bool> CreateProposalAsync(ProjectProposal proposal)
        {
            _context.ProjectProposals.Add(proposal);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProposalAsync(ProjectProposal proposal)
        {
            _context.ProjectProposals.Update(proposal);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> WithdrawProposalAsync(int id, string studentId)
        {
            var proposal = await _context.ProjectProposals
                .FirstOrDefaultAsync(p => p.Id == id && p.StudentId == studentId);

            if (proposal != null && proposal.Status == ProjectStatus.Pending)
            {
                proposal.Status = ProjectStatus.Withdrawn;
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<ProjectProposal>> GetAvailableProposalsForSupervisorAsync(int? areaId, List<int> preferredAreaIds, string? searchTerm = null)
        {
            IQueryable<ProjectProposal> query = _context.ProjectProposals
                .AsNoTracking()
                .Include(p => p.ResearchArea)
                .Where(p => p.Status == ProjectStatus.Pending || p.Status == ProjectStatus.UnderReview);

            if (areaId.HasValue)
            {
                query = query.Where(p => p.ResearchAreaId == areaId.Value);
            }
            else if (preferredAreaIds.Any())
            {
                query = query.Where(p => preferredAreaIds.Contains(p.ResearchAreaId));
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(p => p.Title.ToLower().Contains(searchTerm) || 
                                         p.Abstract.ToLower().Contains(searchTerm) || 
                                         p.TechnicalStack.ToLower().Contains(searchTerm));
            }

            return await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
        }

        public async Task<List<ResearchArea>> GetResearchAreasAsync()
        {
            return await _context.ResearchAreas
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<ResearchArea>> GetSupervisorExpertiseAsync(string supervisorId)
        {
            return await _context.SupervisorExpertises
                .AsNoTracking()
                .Include(se => se.ResearchArea)
                .Where(se => se.SupervisorId == supervisorId)
                .Select(se => se.ResearchArea!)
                .ToListAsync();
        }

        public async Task<bool> UpdateSupervisorExpertiseAsync(string supervisorId, List<int> areaIds)
        {
            var existing = _context.SupervisorExpertises.Where(se => se.SupervisorId == supervisorId);
            _context.SupervisorExpertises.RemoveRange(existing);

            if (areaIds != null)
            {
                foreach (var areaId in areaIds)
                {
                    _context.SupervisorExpertises.Add(new SupervisorExpertise
                    {
                        SupervisorId = supervisorId,
                        ResearchAreaId = areaId
                    });
                }
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
