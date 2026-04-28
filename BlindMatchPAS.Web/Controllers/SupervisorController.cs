using BlindMatchPAS.Core.Entities;
using BlindMatchPAS.Core.Interfaces;
using BlindMatchPAS.Web.Models.Supervisor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlindMatchPAS.Web.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class SupervisorController : Controller
    {
        private readonly IProposalService _proposalService;
        private readonly IMatchService _matchService;
        private readonly UserManager<ApplicationUser> _userManager;

        public SupervisorController(IProposalService proposalService, IMatchService matchService, UserManager<ApplicationUser> userManager)
        {
            _proposalService = proposalService;
            _matchService = matchService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var matchedProjects = await _matchService.GetMatchedProjectsForSupervisorAsync(user.Id);
            var expertise = await _proposalService.GetSupervisorExpertiseAsync(user.Id);

            var viewModel = new SupervisorDashboardViewModel
            {
                MatchedProjects = matchedProjects,
                PreferredResearchAreas = expertise
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ExpertiseManagement()
        {
            var user = await _userManager.GetUserAsync(User);
            var currentExpertise = await _proposalService.GetSupervisorExpertiseAsync(user!.Id);
            var allAreas = await _proposalService.GetResearchAreasAsync();

            var viewModel = new ExpertiseViewModel
            {
                SelectedAreaIds = currentExpertise.Select(a => a.Id).ToList(),
                AllResearchAreas = allAreas
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExpertiseManagement(ExpertiseViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            await _proposalService.UpdateSupervisorExpertiseAsync(user.Id, model.SelectedAreaIds ?? new List<int>());
            
            return RedirectToAction(nameof(Dashboard));
        }

        public async Task<IActionResult> BrowseProposals(int? areaId, string? searchTerm)
        {
            var user = await _userManager.GetUserAsync(User);
            var expertise = await _proposalService.GetSupervisorExpertiseAsync(user!.Id);
            var preferredAreaIds = expertise.Select(a => a.Id).ToList();

            var proposals = await _proposalService.GetAvailableProposalsForSupervisorAsync(areaId, preferredAreaIds, searchTerm);

            var viewModel = new BrowseProposalsViewModel
            {
                AvailableProposals = proposals,
                AllResearchAreas = await _proposalService.GetResearchAreasAsync(),
                FilteredAreaId = areaId,
                SearchTerm = searchTerm
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ProposalDetail(int id)
        {
            var proposal = await _proposalService.GetProposalByIdAsync(id);

            if (proposal == null) return NotFound();

            return View(proposal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExpressInterest(int id)
        {
            await _matchService.ExpressInterestAsync(id);
            return RedirectToAction(nameof(BrowseProposals));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmMatch(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var success = await _matchService.MatchProposalAsync(id, user!.Id);

            if (!success)
            {
                return BadRequest("Proposal already matched or not found.");
            }

            return RedirectToAction(nameof(Dashboard));
        }
    }
}
