using BlindMatchPAS.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlindMatchPAS.Infrastructure.Data
{
    // The database context for the application, handling all database operations
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tables in the database
        public DbSet<ResearchArea> ResearchAreas { get; set; } // Categories of research
        public DbSet<ProjectProposal> ProjectProposals { get; set; } // Student project submissions
        public DbSet<SupervisorExpertise> SupervisorExpertises { get; set; } // Join table for supervisors and areas
        public DbSet<Match> Matches { get; set; } // Records of successful matches
        public DbSet<AuditLog> AuditLogs { get; set; } // Security and activity logs

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
