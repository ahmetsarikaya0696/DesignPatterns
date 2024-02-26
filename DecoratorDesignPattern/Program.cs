using BaseProject;
using DecoratorDesignPattern.Models;
using DecoratorDesignPattern.Repositories;
using DecoratorDesignPattern.Repositories.Decorator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

var connectionString = builder.Configuration.GetConnectionString("designPatternsDb");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString, option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext)).GetName().Name);
    });
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IProductRepository>(serviceProvider =>
{
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
    var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
    var logger = serviceProvider.GetRequiredService<ILogger<ProductRepositoryLogDecorator>>();

    var productRepository = new ProductRepository(context);

    var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache);
    var logDecorator = new ProductRepositoryLogDecorator(cacheDecorator, logger);

    return logDecorator;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    await Helper.SeedDatabase(app);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
