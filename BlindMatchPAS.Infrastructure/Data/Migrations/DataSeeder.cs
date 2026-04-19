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
                    new ResearchArea { Name = "Artificial Intelligence", Description = "Machine Learning, Deep Learning, NLP" },
                    new ResearchArea { Name = "Web Development", Description = "Frontend, Backend, Fullstack technologies" },
                    new ResearchArea { Name = "Cybersecurity", Description = "Network security, Cryptography, Ethical hacking" },
                    new ResearchArea { Name = "Cloud Computing", Description = "AWS, Azure, Cloud architecture" },
                    new ResearchArea { Name = "Data Science", Description = "Data analysis, Visualization, Big Data" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
