using ObserverDesignPattern.Models;

namespace ObserverDesignPattern.Observer
{
    public class UserObserverSubject
    {
        private readonly List<IUserObserver> _userObservers;

        public UserObserverSubject()
        {
            _userObservers = new();
        }

        public void RegisterObserver(IUserObserver observer) => _userObservers.Add(observer);

        public void RemoveObserver(IUserObserver observer) => _userObservers.Remove(observer);

        public void NotifyObservers(ApplicationUser applicationUser)
        {
            _userObservers.ForEach(x =>
            {
                x.UserCreated(applicationUser);
            });
        }
    }
}
