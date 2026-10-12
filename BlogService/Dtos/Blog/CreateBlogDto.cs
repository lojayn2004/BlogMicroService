namespace BlogService.Dtos.Blog
{
    public class CreateBlogDto
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string AuthorAvatarUrl { get; set; } = string.Empty;
    }
}
