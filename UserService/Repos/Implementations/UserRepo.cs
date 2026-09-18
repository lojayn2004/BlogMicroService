using UsersService.Database;
using UsersService.Models;
using UsersService.Repos.Contracts;

namespace UsersService.Repos.Implementations
{
    public class UserRepo(UserDbContext _dbContext) : IUserRepo
    {
        public async Task<bool> CreateAsync(User user)
        {
            _dbContext.Users.Add(user);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
