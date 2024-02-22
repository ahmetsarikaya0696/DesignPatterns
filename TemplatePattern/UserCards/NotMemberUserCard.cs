using TemplatePattern.Models;

namespace TemplatePattern.UserCards
{
    public class NotMemberUserCard : UserCardTemplate
    {
        public NotMemberUserCard(ApplicationUser applicationUser) : base(applicationUser)
        {
        }

        protected override string SetFooter() => string.Empty;

        protected override string SetImage() => $"<img src='/user-images/blank.png' class='card-img-top' alt='user-image'>";
    }
}
