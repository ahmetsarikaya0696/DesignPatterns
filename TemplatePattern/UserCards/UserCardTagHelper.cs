using Microsoft.AspNetCore.Razor.TagHelpers;
using TemplatePattern.Models;

namespace TemplatePattern.UserCards
{
    // <user-card />
    public class UserCardTagHelper : TagHelper
    {
        public ApplicationUser ApplicationUser { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserCardTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            bool isAuthenticated = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

            UserCardTemplate template = isAuthenticated ? new MemberUserCard(ApplicationUser) : new NotMemberUserCard(ApplicationUser);

            output.Content.SetHtmlContent(template.Build());
        }
    }
}
