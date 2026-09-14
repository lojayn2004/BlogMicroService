using BlogService.Dtos.Blog;
using BlogService.Dtos.Result;

namespace BlogService.Services.Contracts
{
    public interface IBlogService
    {

        Task<ServiceResult<IReadOnlyList<BlogDto>>> GetBlogsAsync();

        Task<ServiceResult<BlogDto?>> GetBlogByIdAsync(Guid id);

        Task<ServiceResult<BlogDto>> UpdateBlogAsync(BlogDto blogDto);

        ServiceResult<BlogDto> CreateBlogAsync(CreateBlogDto createBlogDto);

        Task<ServiceResult<string>> DeleteBlogAsync(Guid id, string UserId);

    }
}
