namespace BlogService.Dtos.Blog
{
    public class BlogDto
    {
        public Guid Id { get; set; }

        //public string AuthorId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
