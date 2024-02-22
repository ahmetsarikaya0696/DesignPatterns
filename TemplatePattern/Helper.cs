using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TemplatePattern.Models;

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
                await userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "user1",
                    Email = "user1@outlook.com",
                    ImgUrl = "/user-images/1.jpg",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
                }, "Password12**"); ;
                await userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "user2",
                    Email = "user2@outlook.com",
                    ImgUrl = "/user-images/2.jpg",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
                }, "Password12**");
                await userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "user3",
                    Email = "user3@outlook.com",
                    ImgUrl = "/user-images/3.jpg",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
                }, "Password12**");
                await userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "user4",
                    Email = "user4@outlook.com",
                    ImgUrl = "/user-images/4.jpg",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
                }, "Password12**");
                await userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "user5",
                    Email = "user5@outlook.com",
                    ImgUrl = "/user-images/5.jpg",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
                }, "Password12**");
            }

        }
    }
}
