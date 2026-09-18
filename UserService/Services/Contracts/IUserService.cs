using UsersService.Dtos.Result;
using UsersService.Dtos.Users;


namespace UsersService.Services.Contracts
{
    public interface IUserService
    {
        Task<ServiceResult<UserDto>> CreateAsync(CreateUserDto createUserDto);


        Task<ServiceResult<UserDto>> UpdateAsync(string UserId, UpdateUserDto updateUserDto);

        Task<ServiceResult<string>> DeleteAsync(string  userId);

        Task<ServiceResult<UserDto>> GetByIdAsync(string id);

        Task<ServiceResult<IReadOnlyList<UserDto>>> GetAllAsync();
    }
}
