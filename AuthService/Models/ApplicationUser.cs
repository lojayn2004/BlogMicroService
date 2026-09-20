using Microsoft.AspNetCore.Identity;

namespace AuthService.Models
{
    public class ApplicationUser: IdentityUser
    {
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
