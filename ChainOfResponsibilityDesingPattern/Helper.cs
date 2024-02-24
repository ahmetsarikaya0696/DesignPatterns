using ChainOfResponsibilityDesingPattern.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BaseProject
{
    public static class Helper
    {
        public static async Task SeedDatabase(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var serviceProvider = scope.ServiceProvider;

            var identityDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await Seed.SeedAsync(identityDbContext, userManager);
        }
    }

    public static class Seed
    {
        public static async Task SeedAsync(ApplicationDbContext identityDbContext, UserManager<ApplicationUser> userManager)
        {
            identityDbContext.Database.Migrate();

            if (!await userManager.Users.AnyAsync())
            {
                await userManager.CreateAsync(new ApplicationUser() { UserName = "user1", Email = "user1@outlook.com" }, "Password12**");
                await userManager.CreateAsync(new ApplicationUser() { UserName = "user2", Email = "user2@outlook.com" }, "Password12**");
                await userManager.CreateAsync(new ApplicationUser() { UserName = "user3", Email = "user3@outlook.com" }, "Password12**");
                await userManager.CreateAsync(new ApplicationUser() { UserName = "user4", Email = "user4@outlook.com" }, "Password12**");
                await userManager.CreateAsync(new ApplicationUser() { UserName = "user5", Email = "user5@outlook.com" }, "Password12**");
            }

            if (!await identityDbContext.Products.AnyAsync())
            {
                Enumerable.Range(1, 20).ToList().ForEach(x =>
                {
                    identityDbContext.Products.Add(new Product() { Name = $"Product {x}", Price = 100 * x, Stock = 200 * x })
                });

                await identityDbContext.SaveChangesAsync();
            }
        }
    }
}
