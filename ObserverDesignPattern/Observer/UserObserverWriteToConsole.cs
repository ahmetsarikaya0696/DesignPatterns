using ObserverDesignPattern.Models;

namespace ObserverDesignPattern.Observer
{
    public class UserObserverWriteToConsole : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverWriteToConsole(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UserCreated(ApplicationUser applicationUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverWriteToConsole>>();

            logger.LogInformation($"user created : Id => {applicationUser.Id}");
        }
    }
}
