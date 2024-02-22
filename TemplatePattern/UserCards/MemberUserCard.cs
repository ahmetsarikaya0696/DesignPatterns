using TemplatePattern.Models;

namespace TemplatePattern.UserCards
{
    public class MemberUserCard : UserCardTemplate
    {
        public MemberUserCard(ApplicationUser applicationUser) : base(applicationUser)
        {
        }

        protected override string SetFooter() => @"<div class='d-flex gap-4'>
                                                        <div>
                                                            <a href=""#"" class=""btn btn-primary"">Message</a>
                                                        </div>
                                                        <div>
                                                            <a href=""#"" class=""btn btn-primary"">Profile</a>
                                                        </div>
                                                   </div>";

        protected override string SetImage() => $"<img src='{ApplicationUser.ImgUrl}' class='card-img-top' alt='user-image'>";
    }
}
