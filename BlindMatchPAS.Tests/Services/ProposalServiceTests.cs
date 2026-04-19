using BlindMatchPAS.Core.Entities;
using BlindMatchPAS.Infrastructure.Data;
using BlindMatchPAS.Infrastructure.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlindMatchPAS.Tests.Services
{
    public class ProposalServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreateProposalAsync_ShouldAddProposalToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var service = new ProposalService(context);
            var proposal = new ProjectProposal
            {
                Title = "Test Project",
                Abstract = "Test Abstract",
                TechnicalStack = "C#, .NET",
                StudentId = "student1",
                ResearchAreaId = 1
            };

            // Act
            var result = await service.CreateProposalAsync(proposal);

            // Assert
            result.Should().BeTrue();
            context.ProjectProposals.Count().Should().Be(1);
            var savedProposal = await context.ProjectProposals.FirstAsync();
            savedProposal.Title.Should().Be("Test Project");
        }

        [Fact]
        public async Task GetProposalsForStudentAsync_ShouldReturnOnlyStudentProposals()
        {
            // Arrange
            var context = GetDbContext();
            var service = new ProposalService(context);
            
            var area = new ResearchArea { Id = 1, Name = "Test Area" };
            context.ResearchAreas.Add(area);

            context.ProjectProposals.AddRange(
                new ProjectProposal { Title = "P1", StudentId = "S1", ResearchAreaId = 1 },
                new ProjectProposal { Title = "P2", StudentId = "S1", ResearchAreaId = 1 },
                new ProjectProposal { Title = "P3", StudentId = "S2", ResearchAreaId = 1 }
            );
            await context.SaveChangesAsync();

            // Act
            var results = await service.GetProposalsForStudentAsync("S1");

            // Assert
            results.Should().HaveCount(2);
            results.All(p => p.StudentId == "S1").Should().BeTrue();
        }

        [Fact]
        public async Task WithdrawProposalAsync_ShouldChangeStatusToWithdrawn_WhenPending()
        {
            // Arrange
            var context = GetDbContext();
            var service = new ProposalService(context);
            var proposal = new ProjectProposal
            {
                Id = 10,
                Status = ProjectStatus.Pending,
                StudentId = "S1"
            };
            context.ProjectProposals.Add(proposal);
            await context.SaveChangesAsync();

            // Act
            var result = await service.WithdrawProposalAsync(10, "S1");

            // Assert
            result.Should().BeTrue();
            var updated = await context.ProjectProposals.FindAsync(10);
            updated!.Status.Should().Be(ProjectStatus.Withdrawn);
        }

        [Fact]
        public async Task WithdrawProposalAsync_ShouldReturnFalse_WhenAlreadyMatched()
        {
            // Arrange
            var context = GetDbContext();
            var service = new ProposalService(context);
            var proposal = new ProjectProposal
            {
                Id = 11,
                Status = ProjectStatus.Matched,
                StudentId = "S1"
            };
            context.ProjectProposals.Add(proposal);
            await context.SaveChangesAsync();

            // Act
            var result = await service.WithdrawProposalAsync(11, "S1");

            // Assert
            result.Should().BeFalse();
        }
    }
}
