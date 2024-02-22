using BaseProject;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StrategyDesingPattern.Models;
using StrategyDesingPattern.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("sqlserver");
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

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IProductRepository>(serviceProvider =>
{
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

    var claim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == Settings.claimDatabaseType);
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

    if (claim == null) return new ProductRepositorySqlServer(context);

    var databaseType = (EDatabaseType)Convert.ToInt32(claim.Value);

    var configuration = builder.Configuration;

    return databaseType switch
    {
        EDatabaseType.SqlServer => new ProductRepositorySqlServer(context),
        EDatabaseType.MongoDb => new ProductRepositoryMongoDb(configuration),
        _ => new ProductRepositorySqlServer(context)
    };
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
