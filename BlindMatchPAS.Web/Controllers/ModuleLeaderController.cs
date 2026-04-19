using BlindMatchPAS.Core.Entities;
using BlindMatchPAS.Infrastructure.Data;
using BlindMatchPAS.Web.Models.ModuleLeader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BlindMatchPAS.Web.Controllers
{
    [Authorize(Roles = "ModuleLeader")]
    public class ModuleLeaderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModuleLeaderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var total = await _context.ProjectProposals.CountAsync();
            var pending = await _context.ProjectProposals.CountAsync(p => p.Status == ProjectStatus.Pending);
            var matched = await _context.ProjectProposals.CountAsync(p => p.Status == ProjectStatus.Matched);
            var recent = await _context.ProjectProposals
                .Include(p => p.ResearchArea)
                .Include(p => p.Student)
                .OrderByDescending(p => p.CreatedAt)
                .Take(5)
                .ToListAsync();

            var viewModel = new ModuleLeaderDashboardViewModel
            {
                TotalProposals = total,
                PendingProposals = pending,
                MatchedProposals = matched,
                RecentProposals = recent
            };

            return View(viewModel);
        }

        // Research Area Management
        public async Task<IActionResult> ResearchAreaManagement()
        {
            var areas = await _context.ResearchAreas.ToListAsync();
            return View(areas);
        }

        [HttpGet]
        public IActionResult CreateResearchArea() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateResearchArea(ResearchArea area)
        {
            if (ModelState.IsValid)
            {
                _context.ResearchAreas.Add(area);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ResearchAreaManagement));
            }
            return View(area);
        }

        [HttpGet]
        public async Task<IActionResult> EditResearchArea(int id)
        {
            var area = await _context.ResearchAreas.FindAsync(id);
            if (area == null) return NotFound();
            return View(area);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditResearchArea(ResearchArea area)
        {
            if (ModelState.IsValid)
            {
                _context.Update(area);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ResearchAreaManagement));
            }
            return View(area);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteResearchArea(int id)
        {
            var area = await _context.ResearchAreas.FindAsync(id);
            if (area != null)
            {
                _context.ResearchAreas.Remove(area);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ResearchAreaManagement));
        }

        // Oversight & Reassignment
        public async Task<IActionResult> AllMatches()
        {
            var matches = await _context.Matches
                .Include(m => m.ProjectProposal)
                .Include(m => m.Student)
                .Include(m => m.Supervisor)
                .ToListAsync();

            var viewModel = new AllMatchesViewModel { Matches = matches };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reassign(int matchId)
        {
            var match = await _context.Matches.FindAsync(matchId);
            if (match != null)
            {
                var proposal = await _context.ProjectProposals.FindAsync(match.ProjectProposalId);
                if (proposal != null)
                {
                    proposal.Status = ProjectStatus.Pending;
                }
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(AllMatches));
        }
    }
}

