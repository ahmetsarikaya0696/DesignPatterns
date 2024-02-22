using ObserverDesignPattern.Models;

namespace ObserverDesignPattern.Observer
{
    public class UserObserverCreateDiscount : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverCreateDiscount(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UserCreated(ApplicationUser applicationUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();

            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Discounts.Add(new Discount() { Rate = 10, UserId = applicationUser.Id });
            context.SaveChanges();

            logger.LogInformation("Discount created");
        }
    }
}
