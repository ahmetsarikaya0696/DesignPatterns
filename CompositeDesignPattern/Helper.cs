using CompositeDesignPattern.Models;
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

            ApplicationUser user1 = new ApplicationUser() { UserName = "user1", Email = "user1@outlook.com" };

            if (!await userManager.Users.AnyAsync())
            {
                await userManager.CreateAsync(user1, "Password12**");
                await userManager.CreateAsync(new ApplicationUser() { UserName = "user2", Email = "user2@outlook.com" }, "Password12**");
                await userManager.CreateAsync(new ApplicationUser() { UserName = "user3", Email = "user3@outlook.com" }, "Password12**");
                await userManager.CreateAsync(new ApplicationUser() { UserName = "user4", Email = "user4@outlook.com" }, "Password12**");
                await userManager.CreateAsync(new ApplicationUser() { UserName = "user5", Email = "user5@outlook.com" }, "Password12**");
            }


            if (!await identityDbContext.Categories.AnyAsync())
            {
                var category1 = new Category() { Name = "Suç", UserId = user1.Id };
                var category2 = new Category() { Name = "Polisiye", UserId = user1.Id };
                var category3 = new Category() { Name = "Cinayet", UserId = user1.Id };

                await identityDbContext.Categories.AddRangeAsync(category1, category2, category3);
                await identityDbContext.SaveChangesAsync();

                var subCategory1 = new Category() { Name = "Suç 1", UserId = user1.Id, ReferenceId = category1.Id };
                var subCategory2 = new Category() { Name = "Cinayet 1", UserId = user1.Id, ReferenceId = category2.Id };
                var subCategory3 = new Category() { Name = "Polisiye 1", UserId = user1.Id, ReferenceId = category3.Id };

                await identityDbContext.Categories.AddRangeAsync(subCategory1, subCategory2, subCategory3);
                await identityDbContext.SaveChangesAsync();

                var subSubCategory1 = new Category() { Name = "Polisiye 1.1", UserId = user1.Id, ReferenceId = subCategory1.Id };

                await identityDbContext.Categories.AddAsync(subSubCategory1);
                await identityDbContext.SaveChangesAsync();
            }
        }
    }
}
