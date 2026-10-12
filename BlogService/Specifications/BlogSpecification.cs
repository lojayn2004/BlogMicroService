using BlogService.Dtos.Blog;
using BlogService.Models;

namespace BlogService.Specifications
{
    public class BlogSpecification: BaseSpeification<Blog>


    {
        public BlogSpecification(BlogQuerySearch blogQuerySearch):
            base(x => (string.IsNullOrEmpty(blogQuerySearch.AuthorId) || x.AuthorId == blogQuerySearch.AuthorId))
        {
            ApplyPaging(blogQuerySearch.PageSize ?? 10, blogQuerySearch.PageNumber ?? 1);
        }
    }
}
