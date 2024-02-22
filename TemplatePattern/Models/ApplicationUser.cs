using Microsoft.AspNetCore.Identity;

namespace TemplatePattern.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ImgUrl { get; set; }
        public string Description { get; set; }
    }
}
