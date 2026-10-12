using BlogService.Models;
using BlogService.Specifications;

namespace BlogService.Repos.Contracts
{
    public interface IBlogRepo
    {
        void Create(Blog blog);

        void Update(Blog blog);

        void Delete(Blog blog);

        Task<IReadOnlyList<Blog>> GetAllAsync();

        Task<IReadOnlyList<Blog>> GetAllAsync(BaseSpeification<Blog> speification);

        Task<Blog?> GetByIdAsync(Guid id);
    }
}
