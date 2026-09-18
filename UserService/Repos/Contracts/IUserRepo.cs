using UsersService.Models;

namespace UsersService.Repos.Contracts
{
    public interface IUserRepo
    {
        Task<User> GetByIdAsync(string userId);

        Task<bool> CreateAsync(User user);
       
        Task<bool> UpdateAsync(User user);

        Task<bool>  DeleteAsync(User user);
    }
}
