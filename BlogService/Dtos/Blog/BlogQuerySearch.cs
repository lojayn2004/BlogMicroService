using MassTransit.Internals.GraphValidation;

namespace BlogService.Dtos.Blog
{
    public class BlogQuerySearch
    {
        public int? PageNumber { get; set; } = 1;

        public int? PageSize { get; set; } = 10;

        public string? AuthorId { get; set; }




    }
}
