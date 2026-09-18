namespace UsersService.Dtos.Users
{
    public class CreateUserDto
    {
        public string UserId { get; set; }

        public string DisplayName { get; set; }

        public string Bio { get; set; }

        public string AvatarUrl { get; set; }

    }
}
