using Microsoft.AspNetCore.Identity;

namespace UsersService.Models
{
    public class ApplicationUser: IdentityUser
    {
        // // UserId, DisplayName, Bio, AvatarUrl, CreatedAt

        public string DisplayName { get; set; }

        public string Bio { get; set; }

        public string AvatarUrl { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
