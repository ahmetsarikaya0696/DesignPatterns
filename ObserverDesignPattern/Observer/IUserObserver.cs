using ObserverDesignPattern.Models;

namespace ObserverDesignPattern.Observer
{
    public interface IUserObserver
    {
        void UserCreated(ApplicationUser applicationUser);
    }
}
