using BlogService.Dtos.Blog;
using BlogService.Dtos.Result;

namespace BlogService.Services.Contracts
{
    public interface IBlogService
    {

        Task<ServiceResult<IReadOnlyList<BlogDto>>> GetBlogsAsync(BlogQuerySearch blogQuerySearch);

        Task<ServiceResult<BlogDto?>> GetBlogByIdAsync(Guid id);

        ServiceResult<BlogDto> CreateBlogAsync(string userId, CreateBlogDto createBlogDto);

        Task<ServiceResult<BlogDto>> UpdateBlogAsync(string userId, BlogDto blogDto);

      
        Task<ServiceResult<string>> DeleteBlogAsync(string userId, Guid blogId);

    }
}
