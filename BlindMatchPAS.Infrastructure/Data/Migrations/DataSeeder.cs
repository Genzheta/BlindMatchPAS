using BlindMatchPAS.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlindMatchPAS.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Roles
            string[] roles = { "Admin", "ModuleLeader", "Supervisor", "Student" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Seed Admin User
            var adminEmail = "admin@pas.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Admin",
                    Role = "Admin",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Seed Research Areas
            if (!context.ResearchAreas.Any())
            {
                context.ResearchAreas.AddRange(
                    new ResearchArea { Name = "Intelligent Systems & Robotics", Description = "Autonomous agents, Computer Vision, and Neural Networks" },
                    new ResearchArea { Name = "Software Engineering & Quality", Description = "Testing automation, CI/CD, and legacy refactoring" },
                    new ResearchArea { Name = "Network Security & Forensics", Description = "Intrusion detection, Malware analysis, and Cryptography" },
                    new ResearchArea { Name = "Distributed Cloud Systems", Description = "Microservices, Serverless, and Edge computing" },
                    new ResearchArea { Name = "Advanced Data Analytics", Description = "Predictive modeling, Big Data ethics, and Visualization" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
