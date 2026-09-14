using AutoMapper;
using BlogService.Dtos.Blog;
using BlogService.Dtos.Result;
using BlogService.Models;
using BlogService.Repos.Contracts;
using BlogService.Services.Contracts;

namespace BlogService.Services.Implementations
{
    public class BlogService(IBlogRepo _blogRepo, IMapper _mapper): IBlogService
    {
        public ServiceResult<BlogDto> CreateBlogAsync(CreateBlogDto createBlogDto)
        {
            var blog = _mapper.Map<Blog>(createBlogDto);
            _blogRepo.Create(blog);
            return ServiceResult<BlogDto>.Success(_mapper.Map<BlogDto>(blog));
        }

        public async Task<ServiceResult<string>> DeleteBlogAsync(Guid id, string UserId)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog == null)
                return ServiceResult<string>.Faliure(ResultType.NotFound, $"Blog with id {id} No Found");
             _blogRepo.Delete(blog);
            return ServiceResult<string>.Success($"Blog With Id {id} Deleted Successfully");
        }

        public async Task<ServiceResult<BlogDto?>> GetBlogByIdAsync(Guid id)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog == null)
                return ServiceResult<BlogDto?>.Faliure(ResultType.NotFound, $"Blog with id {id} No Found");
        
            return ServiceResult<BlogDto?>.Success(_mapper.Map<BlogDto?>(blog));
        }


        public async Task<ServiceResult<IReadOnlyList<BlogDto>>> GetBlogsAsync()
        {
            var blogs = await _blogRepo.GetAllAsync();
            var mappedBlogs = _mapper.Map<IReadOnlyList<BlogDto>>(blogs);
            return ServiceResult<IReadOnlyList <BlogDto>>.Success(mappedBlogs);
        }

        public async Task<ServiceResult<BlogDto>> UpdateBlogAsync(BlogDto blogDto)
        {
            var blog = await _blogRepo.GetByIdAsync(blogDto.Id);
            if (blog == null)
                return ServiceResult<BlogDto>.Faliure(ResultType.NotFound, $"Blog with id {blogDto.Id} No Found");
             _mapper.Map(blogDto, blog);
            var mappedBlog = _mapper.Map<BlogDto>(blog);
            return ServiceResult<BlogDto>.Success(mappedBlog);
        }
    }
}
