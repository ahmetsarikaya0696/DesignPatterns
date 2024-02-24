using MediatR;
using ObserverDesignPattern.Models;

namespace ObserverDesignPattern.Notifications
{
    public class UserCreatedNotification : INotification
    {
        public ApplicationUser ApplicationUser { get; set; }

        public class UserCreatedWriteToConsoleNotificationHandler : INotificationHandler<UserCreatedNotification>
        {
            private readonly ILogger<UserCreatedWriteToConsoleNotificationHandler> _logger;

            public UserCreatedWriteToConsoleNotificationHandler(ILogger<UserCreatedWriteToConsoleNotificationHandler> logger)
            {
                _logger = logger;
            }

            public Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"user created : Id => {notification.ApplicationUser.Id}");

                return Task.CompletedTask;
            }
        }

        public class UserCreatedCreateDiscountNotificationHandler : INotificationHandler<UserCreatedNotification>
        {
            private readonly ApplicationDbContext _context;
            private readonly ILogger<UserCreatedCreateDiscountNotificationHandler> _logger;

            public UserCreatedCreateDiscountNotificationHandler(ApplicationDbContext context, ILogger<UserCreatedCreateDiscountNotificationHandler> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
            {
                await _context.Discounts.AddAsync(new Discount() { Rate = 10, UserId = notification.ApplicationUser.Id });
                await _context.SaveChangesAsync();

                _logger.LogInformation("Discount created");
            }
        }

        public class UserCreatedSendEmailNotificationHandler : INotificationHandler<UserCreatedNotification>
        {
            private readonly ILogger<UserCreatedSendEmailNotificationHandler> _logger;

            public UserCreatedSendEmailNotificationHandler(ILogger<UserCreatedSendEmailNotificationHandler> logger)
            {
                _logger = logger;
            }

            public Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
            {

                // Email gönderildiğini varsay

                string logMessage = $"Welcome email send to user : {notification.ApplicationUser.UserName}";

                _logger.LogInformation(logMessage);

                return Task.CompletedTask;
            }
        }
    }
}
