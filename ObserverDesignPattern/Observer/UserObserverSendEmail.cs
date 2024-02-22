using ObserverDesignPattern.Models;

namespace ObserverDesignPattern.Observer
{
    public class UserObserverSendEmail : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverSendEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UserCreated(ApplicationUser applicationUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendEmail>>();

            // Email gönderildiğini varsay

            string logMessage = $"Welcome email send to user : {applicationUser.UserName}";

            logger.LogInformation(logMessage);
        }
    }
}
