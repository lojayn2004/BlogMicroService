using AutoMapper;
using BlogService.Dtos.Blog;
using BlogService.Models;

namespace BlogService.Mappers
{
    public class BlogProfile: Profile
    {
        public BlogProfile()
        {
            CreateMap<CreateBlogDto, BlogDto>();

            CreateMap<Blog, BlogDto>();
        }
    }
}
