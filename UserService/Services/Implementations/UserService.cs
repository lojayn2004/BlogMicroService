using Microsoft.AspNetCore.Identity;
using UsersService.Dtos.Result;
using UsersService.Dtos.Users;
using UsersService.Models;
using UsersService.Services.Contracts;

namespace UsersService.Services.Implementations
{
    public class UserService(UserManager<ApplicationUser> _userManager) : IUserService
    {
        public async Task<ServiceResult<UserDto>> CreateAsync(CreateUserDto createUserDto)
        {
            var user = await _userManager.FindByEmailAsync(createUserDto.Email);
            if(user != null)
                return ServiceResult<UserDto>.Faliure(ResultType.Validation, "User with this email already exists.");

            var applicationUser = new ApplicationUser
            {
                UserName = createUserDto.UserName,
                Email = createUserDto.Email,
                DisplayName = createUserDto.DisplayName,
                Bio = createUserDto.Bio,
                AvatarUrl = createUserDto.AvatarUrl
            };

            var result = await _userManager.CreateAsync(applicationUser, createUserDto.Password);
            if(!result.Succeeded)
                return ServiceResult<UserDto>.Faliure(ResultType.Validation, "Failed to create user.");

            return ServiceResult<UserDto>.Success(new UserDto
            {
                Id = applicationUser.Id,
                UserName = applicationUser.UserName,
                Email = applicationUser.Email,
                DisplayName = applicationUser.DisplayName,
                Bio = applicationUser.Bio,
                AvatarUrl = applicationUser.AvatarUrl
            });
        }

        public async Task<ServiceResult<string>> DeleteAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
                return ServiceResult<string>.Faliure(ResultType.NotFound, "User not found.");
            var result = await _userManager.DeleteAsync(user);
            if(!result.Succeeded)
                return ServiceResult<string>.Faliure(ResultType.UnAuthorized, "Failed to delete user.");
            return ServiceResult<string>.Success("User deleted successfully.");
        }

        public Task<ServiceResult<IReadOnlyList<UserDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<UserDto>> GetByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ServiceResult<UserDto>.Faliure(ResultType.NotFound, "User not found.");
            return ServiceResult<UserDto>.Success(new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Bio = user.Bio,
                AvatarUrl = user.AvatarUrl
            });
        }

        public async Task<ServiceResult<UserDto>> UpdateAsync(string userId, UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ServiceResult<UserDto>.Faliure(ResultType.NotFound, "User not found.");

            user.UserName = updateUserDto.UserName;
            user.DisplayName = updateUserDto.DisplayName;
            user.Bio = updateUserDto.Bio;
            user.AvatarUrl = updateUserDto.AvatarUrl;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return ServiceResult<UserDto>.Faliure(ResultType.Validation, "Failed to update user.");

            return ServiceResult<UserDto>.Success(new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Bio = user.Bio,
                AvatarUrl = user.AvatarUrl
            });
        }
    }
}

