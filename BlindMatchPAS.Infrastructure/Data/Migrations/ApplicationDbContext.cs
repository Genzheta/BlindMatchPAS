using BlindMatchPAS.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlindMatchPAS.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ResearchArea> ResearchAreas { get; set; }
        public DbSet<ProjectProposal> ProjectProposals { get; set; }
        public DbSet<SupervisorExpertise> SupervisorExpertises { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure SupervisorExpertise Join Table
            builder.Entity<SupervisorExpertise>()
                .HasKey(se => new { se.SupervisorId, se.ResearchAreaId });

            builder.Entity<SupervisorExpertise>()
                .HasOne(se => se.Supervisor)
                .WithMany(u => u.Expertises)
                .HasForeignKey(se => se.SupervisorId);

            builder.Entity<SupervisorExpertise>()
                .HasOne(se => se.ResearchArea)
                .WithMany(ra => ra.SupervisorExpertises)
                .HasForeignKey(se => se.ResearchAreaId);

            // Configure Match relationships
            builder.Entity<Match>()
                .HasOne(m => m.ProjectProposal)
                .WithOne(p => p.Match)
                .HasForeignKey<Match>(m => m.ProjectProposalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Match>()
                .HasOne(m => m.Student)
                .WithMany(u => u.MatchesAsStudent)
                .HasForeignKey(m => m.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Match>()
                .HasOne(m => m.Supervisor)
                .WithMany(u => u.MatchesAsSupervisor)
                .HasForeignKey(m => m.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure ProjectProposal relationships
            builder.Entity<ProjectProposal>()
                .HasOne(p => p.Student)
                .WithMany(u => u.Proposals)
                .HasForeignKey(p => p.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectProposal>()
                .HasOne(p => p.ResearchArea)
                .WithMany(ra => ra.Proposals)
                .HasForeignKey(p => p.ResearchAreaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
