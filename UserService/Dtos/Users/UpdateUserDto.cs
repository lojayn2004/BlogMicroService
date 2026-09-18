namespace UsersService.Dtos.Users
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public string Bio { get; set; }

        public string AvatarUrl { get; set; }
    }
}
