using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using AuthService.Dtos.Auth;
using AuthService.Dtos.Result;
using AuthService.Services.ServicesAbstractions;
using AuthService.Utils;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using Shared.Events;

namespace AuthService.Services.Implementations
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, 
         IOptions<JwtConfigurations> _jwtOptions,
         IRequestClient<UserCreatedEvent> _requestClient): IAuthenticationService
    {
        public async Task<ServiceResult<AuthResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return ServiceResult<AuthResponseDto>.Error(ServiceError.UnAuthorized, "Incorrect Email Or Password");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return ServiceResult<AuthResponseDto>.Error(ServiceError.UnAuthorized, "Incorrect Email Or Password");
           
            var authDto = await GetAuthResponseDtoAsync(user);

          
            return ServiceResult<AuthResponseDto>.Ok(authDto);
        }

        public async Task<ServiceResult<AuthResponseDto>> RefreshTokenAsync(string refreshToken)
        {
            var authModel = new AuthResponseDto();
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
            if (user == null)
                return ServiceResult<AuthResponseDto>.Error(ServiceError.UnAuthorized, "Invalid Token");

            var refreshTokenEntity = user.RefreshTokens.SingleOrDefault(t => t.Token == refreshToken);
            if(!(refreshTokenEntity.IsActive))
                return ServiceResult<AuthResponseDto>.Error(ServiceError.UnAuthorized, "Token Expired");

            refreshTokenEntity.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = JwtHelper.GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            authModel.UserId = user.Id;
            authModel.Email = user.Email;
            authModel.Token = await JwtHelper.GenerateJwtTokenAsync(user, _userManager, _jwtOptions);
            authModel.RefreshToken = newRefreshToken.Token;
            authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;

            return ServiceResult<AuthResponseDto>.Ok(authModel);

        }

        public async Task<ServiceResult<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null)
                return ServiceResult<AuthResponseDto>.Error(ServiceError.Conflict, "Email Already Registered");
            var applicationUser = new ApplicationUser()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
               
            };
            var result = await _userManager.CreateAsync(applicationUser, registerDto.Password);
          
            if(!result.Succeeded)
            {
                string  errors  = string.Join(", ", result.Errors.Select(e => e.Description));
                return ServiceResult<AuthResponseDto>.Error(ServiceError.Validation, errors);
                
            }

            // call user service to add the user and wait for response
            var response = await _requestClient.GetResponse<UserCreatedResponse>(new UserCreatedEvent
            {
                UserId = applicationUser.Id,
                Email = applicationUser.Email,
                UserName = applicationUser.UserName,
                Bio = registerDto.Bio,
                AvatarUrl = registerDto.AvatarUrl
            });

            if (!response.Message.IsSuccess)
            {
                // Delete user from identity since profile creation failed
                await _userManager.DeleteAsync(applicationUser);
                return ServiceResult<AuthResponseDto>.Error(ServiceError.Validation, $"Failed to create user profile: {response.Message.ErrorMessage}");
            }

            string token = await JwtHelper.GenerateJwtTokenAsync(applicationUser, _userManager, _jwtOptions);
           

            var authDto =  new AuthResponseDto
            {
                UserId = applicationUser.Id,
                Email = applicationUser.Email,
                Token = token
            };
            return ServiceResult<AuthResponseDto>.Ok(authDto);
        }



        private async Task<AuthResponseDto> GetAuthResponseDtoAsync(ApplicationUser user)
        {
            var authDto = new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Token = await JwtHelper.GenerateJwtTokenAsync(user, _userManager, _jwtOptions)
            };
            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authDto.RefreshToken = activeRefreshToken.Token;
                authDto.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = JwtHelper.GenerateRefreshToken();
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
                authDto.RefreshToken = refreshToken.Token;
                authDto.RefreshTokenExpiration = refreshToken.ExpiresOn;
            }
            return authDto;
        }
     
    }
}
