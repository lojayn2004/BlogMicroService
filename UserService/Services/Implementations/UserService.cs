using UsersService.Dtos.Result;
using UsersService.Dtos.Users;
using UsersService.Models;
using UsersService.Repos.Contracts;
using UsersService.Services.Contracts;

namespace UsersService.Services.Implementations
{
    public class UserService(IUserRepo _userRepo) : IUserService
    {
        public async Task<ServiceResult<UserDto>> CreateAsync(CreateUserDto createUserDto)
        {

            var user = new User
            {
                UserId = createUserDto.UserId,
                DisplayName = createUserDto.DisplayName,
                Bio = createUserDto.Bio,
                AvatarUrl = createUserDto.AvatarUrl
            };

            var creationSucceded = await _userRepo.CreateAsync(user);
            if(!creationSucceded)
                return ServiceResult<UserDto>.Faliure(ResultType.Validation, "Failed to create user.");

            return ServiceResult<UserDto>.Success(new UserDto
            {
                Id = user.UserId,
                DisplayName = user.DisplayName,
                Bio = user.Bio,
                AvatarUrl = user.AvatarUrl
            });
        }

        public async Task<ServiceResult<string>> DeleteAsync(string userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if(user == null)
                return ServiceResult<string>.Faliure(ResultType.NotFound, "User not found.");
            var result = await _userRepo.DeleteAsync(user);
            if(!result)
                return ServiceResult<string>.Faliure(ResultType.UnAuthorized, "Failed to delete user.");
            return ServiceResult<string>.Success("User deleted successfully.");
        }

        public Task<ServiceResult<IReadOnlyList<UserDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<UserDto>> GetByIdAsync(string userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                return ServiceResult<UserDto>.Faliure(ResultType.NotFound, "User not found.");
            return ServiceResult<UserDto>.Success(new UserDto
            {
                Id = user.UserId,
                DisplayName = user.DisplayName,
                Bio = user.Bio,
                AvatarUrl = user.AvatarUrl
            });
        }

        public async Task<ServiceResult<UserDto>> UpdateAsync(string userId, UpdateUserDto updateUserDto)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                return ServiceResult<UserDto>.Faliure(ResultType.NotFound, "User not found.");

        
            user.DisplayName = updateUserDto.DisplayName;
            user.Bio = updateUserDto.Bio;
            user.AvatarUrl = updateUserDto.AvatarUrl;

            var result = await _userRepo.UpdateAsync(user);
            if (!result) 
                return ServiceResult<UserDto>.Faliure(ResultType.Validation, "Failed to update user.");

            return ServiceResult<UserDto>.Success(new UserDto
            {
                Id = user.UserId,
           
                DisplayName = user.DisplayName,
                Bio = user.Bio,
                AvatarUrl = user.AvatarUrl
            });
        }
    }
}

