using System.Text;
using TemplatePattern.Models;

namespace TemplatePattern.UserCards
{
    public abstract class UserCardTemplate
    {
        protected UserCardTemplate(ApplicationUser applicationUser)
        {
            ApplicationUser = applicationUser;
        }

        protected ApplicationUser ApplicationUser { get; set; }

        public string Build()
        {
            if (ApplicationUser == null) throw new ArgumentNullException(nameof(ApplicationUser));

            var stringBuilder = new StringBuilder();

            stringBuilder.Append("<div class='card'>");

            stringBuilder.Append(SetImage());

            stringBuilder.Append($@"<div class='card-body'>
                                                <h5 class='card-title'>{ApplicationUser.UserName}</h5>
                                                <p class='card-text'>{ApplicationUser.Description}</p>");
            stringBuilder.Append(SetFooter());

            stringBuilder.Append("</div>");

            stringBuilder.Append("</div>");

            return stringBuilder.ToString();
        }

        protected abstract string SetFooter();
        protected abstract string SetImage();
    }
}
