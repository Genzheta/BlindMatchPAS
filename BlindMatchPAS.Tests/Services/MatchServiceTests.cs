using BlindMatchPAS.Core.Entities;
using BlindMatchPAS.Infrastructure.Data;
using BlindMatchPAS.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlindMatchPAS.Tests.Services
{
    public class MatchServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task MatchProposalAsync_ShouldReturnTrue_WhenProposalIsPending()
        {
            var context = GetDbContext();
            var service = new MatchService(context);
            
            var proposal = new ProjectProposal
            {
                Id = 1,
                Title = "Test Proposal",
                Status = ProjectStatus.Pending,
                StudentId = "student1"
            };
            context.ProjectProposals.Add(proposal);
            await context.SaveChangesAsync();

            var result = await service.MatchProposalAsync(1, "supervisor1");

            result.Should().BeTrue();
            var updatedProposal = await context.ProjectProposals.FindAsync(1);
            updatedProposal!.Status.Should().Be(ProjectStatus.Matched);
            
            var match = await context.Matches.FirstOrDefaultAsync(m => m.ProjectProposalId == 1);
            match.Should().NotBeNull();
            match!.SupervisorId.Should().Be("supervisor1");
        }

        [Fact]
        public async Task MatchProposalAsync_ShouldReturnFalse_WhenProposalIsAlreadyMatched()
        {
            var context = GetDbContext();
            var service = new MatchService(context);
            
            var proposal = new ProjectProposal
            {
                Id = 2,
                Title = "Matched Proposal",
                Status = ProjectStatus.Matched,
                StudentId = "student2"
            };
            context.ProjectProposals.Add(proposal);
            await context.SaveChangesAsync();

            var result = await service.MatchProposalAsync(2, "supervisor2");

            result.Should().BeFalse();
        }

        [Fact]
        public async Task MatchProposalAsync_ShouldReturnFalse_WhenProposalDoesNotExist()
        {
            var context = GetDbContext();
            var service = new MatchService(context);

            var result = await service.MatchProposalAsync(999, "supervisor3");

            result.Should().BeFalse();
        }
    }
}
