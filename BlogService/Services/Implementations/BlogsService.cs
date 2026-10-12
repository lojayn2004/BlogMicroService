using AutoMapper;
using BlogService.Dtos.Blog;
using BlogService.Dtos.Result;
using BlogService.Models;
using BlogService.Repos.Contracts;
using BlogService.Services.Contracts;
using BlogService.Specifications;

namespace BlogService.Services.Implementations
{
    public class BlogsService(IBlogRepo _blogRepo, IMapper _mapper): IBlogService
    {
        public ServiceResult<BlogDto> CreateBlogAsync(string userId, string authorName, CreateBlogDto createBlogDto)
        {
            var blog = _mapper.Map<Blog>(createBlogDto);
            blog.AuthorId = userId;
            blog.AuthorName = authorName;
            blog.AuthorAvatarUrl = createBlogDto.AuthorAvatarUrl;
            _blogRepo.Create(blog);
            return ServiceResult<BlogDto>.Success(_mapper.Map<BlogDto>(blog));
        }

        public async Task<ServiceResult<string>> DeleteBlogAsync(string userId, Guid blogId)
        {
            var blog = await _blogRepo.GetByIdAsync(blogId);
            if (blog == null)
                return ServiceResult<string>.Faliure(ResultType.NotFound, $"Blog with id {blogId} No Found");
            if (blog.AuthorId != userId)
                return ServiceResult<string>.Faliure(ResultType.UnAuthorized, $"You are not authorized to update this blog");

            _blogRepo.Delete(blog);
            return ServiceResult<string>.Success($"Blog With Id {blogId} Deleted Successfully");
        }

        public async Task<ServiceResult<BlogDto?>> GetBlogByIdAsync(Guid id)
        {
            var blog = await _blogRepo.GetByIdAsync(id);
            if (blog == null)
                return ServiceResult<BlogDto?>.Faliure(ResultType.NotFound, $"Blog with id {id} No Found");
        
            return ServiceResult<BlogDto?>.Success(_mapper.Map<BlogDto?>(blog));
        }


        public async Task<ServiceResult<IReadOnlyList<BlogDto>>> GetBlogsAsync(BlogQuerySearch blogQuerySearch)
        {
            var blogs = await _blogRepo.GetAllAsync(new BlogSpecification(blogQuerySearch));
            var mappedBlogs = _mapper.Map<IReadOnlyList<BlogDto>>(blogs);
            return ServiceResult<IReadOnlyList <BlogDto>>.Success(mappedBlogs);
        }

        public async Task<ServiceResult<BlogDto>> UpdateBlogAsync(string userId, BlogDto blogDto)
        {
            var blog = await _blogRepo.GetByIdAsync(blogDto.Id);
            if (blog == null)
                return ServiceResult<BlogDto>.Faliure(ResultType.NotFound, $"Blog with id {blogDto.Id} No Found");
             
            if(blog.AuthorId != userId)
                return ServiceResult<BlogDto>.Faliure(ResultType.UnAuthorized, $"You are not authorized to update this blog");
           
            _mapper.Map(blogDto, blog);
            var mappedBlog = _mapper.Map<BlogDto>(blog);
            return ServiceResult<BlogDto>.Success(mappedBlog);
        }
    }
}
