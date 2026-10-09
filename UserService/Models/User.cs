

namespace UsersService.Models
{
    public class User
    {
        public string UserId { get; set; }

        public string DisplayName { get; set; }

        public string Bio { get; set; }

        public string AvatarUrl { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
