using AutoMapper;
using BlogService.Dtos.Blog;
using BlogService.Models;

namespace BlogService.Mappers
{
    public class BlogProfile: Profile
    {
        public BlogProfile()
        {
            CreateMap<CreateBlogDto, Blog>();


            CreateMap<Blog, BlogDto>();
            CreateMap<BlogDto, Blog>()
                .ForMember(blog => blog.AuthorId, options => options.Ignore())
                .ForMember(blog => blog.AuthorName, options => options.Ignore())
                .ForMember(blog => blog.AuthorAvatarUrl, options => options.Ignore())
                .ForMember(blog => blog.CreatedAt, options => options.Ignore());
        }
    }
}
