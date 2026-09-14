using Azure;
using BlogService.Database.Contexts;
using BlogService.Models;
using BlogService.Repos.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BlogService.Repos.Implementations
{
    public class BlogRepo(BlogDbContext _blogContext) : IBlogRepo
    {
        public void Create(Blog blog)
        {
            _blogContext.Blogs.Add(blog);
            _blogContext.SaveChanges();
        }

        public void Delete(Blog blog)
        {
            _blogContext.Blogs.Remove(blog);
            _blogContext.SaveChanges();
        }

        public async Task<IReadOnlyList<Blog>> GetAllAsync()
        {
            return await _blogContext.Blogs.ToListAsync();
        }

        public async Task<Blog?> GetByIdAsync(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public void Update(Blog blog)
        {
            _blogContext.Blogs.Update(blog);
            _blogContext.SaveChanges();
        }
    }
}
