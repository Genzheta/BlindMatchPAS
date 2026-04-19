using BlindMatchPAS.Core.Entities;
using BlindMatchPAS.Core.Interfaces;
using BlindMatchPAS.Web.Models.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlindMatchPAS.Web.Controllers
{
    // Handles all student-related actions like viewing the dashboard and managing proposals
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IProposalService _proposalService;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(IProposalService proposalService, UserManager<ApplicationUser> userManager)
        {
            _proposalService = proposalService;
            _userManager = userManager;
        }

        // Displays the student's dashboard with their project proposals
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var proposals = await _proposalService.GetProposalsForStudentAsync(user.Id);

            var viewModel = new StudentDashboardViewModel
            {
                Proposals = proposals
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new ProjectProposalViewModel
            {
                ResearchAreas = await _proposalService.GetResearchAreasAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectProposalViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            if (ModelState.IsValid)
            {
                var proposal = new ProjectProposal
                {
                    Title = model.Title,
                    Abstract = model.Abstract,
                    TechnicalStack = model.TechnicalStack,
                    ResearchAreaId = model.ResearchAreaId,
                    StudentId = user.Id,
                    Status = ProjectStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                };

                await _proposalService.CreateProposalAsync(proposal);
                return RedirectToAction(nameof(Dashboard));
            }

            model.ResearchAreas = await _proposalService.GetResearchAreasAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var proposal = await _proposalService.GetProposalByIdForStudentAsync(id, user!.Id);

            if (proposal == null || proposal.Status == ProjectStatus.Matched)
            {
                return NotFound("Proposal not found or already matched.");
            }

            var viewModel = new ProjectProposalViewModel
            {
                Id = proposal.Id,
                Title = proposal.Title,
                Abstract = proposal.Abstract,
                TechnicalStack = proposal.TechnicalStack,
                ResearchAreaId = proposal.ResearchAreaId,
                ResearchAreas = await _proposalService.GetResearchAreasAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectProposalViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var proposal = await _proposalService.GetProposalByIdAsync(model.Id ?? 0);

            if (proposal == null || proposal.StudentId != user!.Id || proposal.Status == ProjectStatus.Matched)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                proposal.Title = model.Title;
                proposal.Abstract = model.Abstract;
                proposal.TechnicalStack = model.TechnicalStack;
                proposal.ResearchAreaId = model.ResearchAreaId;

                await _proposalService.UpdateProposalAsync(proposal);
                return RedirectToAction(nameof(Dashboard));
            }

            model.ResearchAreas = await _proposalService.GetResearchAreasAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            await _proposalService.WithdrawProposalAsync(id, user!.Id);
            return RedirectToAction(nameof(Dashboard));
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var proposal = await _proposalService.GetProposalByIdAsync(id);

            if (proposal == null || proposal.StudentId != user!.Id) return NotFound();

            return View(proposal);
        }
    }
}
