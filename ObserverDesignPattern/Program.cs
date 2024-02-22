using BaseProject;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ObserverDesignPattern.Models;
using ObserverDesignPattern.Observer;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


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

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

builder.Services.AddSingleton(serviceProvider =>
{
    UserObserverSubject userObserverSubject = new();

    userObserverSubject.RegisterObserver(new UserObserverWriteToConsole(serviceProvider));
    userObserverSubject.RegisterObserver(new UserObserverCreateDiscount(serviceProvider));
    userObserverSubject.RegisterObserver(new UserObserverSendEmail(serviceProvider));

    return userObserverSubject;
});

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