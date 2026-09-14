using BlogService.Models;

namespace BlogService.Repos.Contracts
{
    public interface IBlogRepo
    {
        void Create(Blog blog);

        void Update(Blog blog);

        void Delete(Blog blog);

        Task<IReadOnlyList<Blog>> GetAllAsync();

        Task<Blog?> GetByIdAsync(Guid id);
    }
}
