using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using AuthService.Dtos.Auth;
using AuthService.Dtos.Result;
using AuthService.Services.ServicesAbstractions;
using AuthService.Utils;
using AuthService.Models;


namespace AuthService.Services.Implementations
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, 
         IOptions<JwtConfigurations> _jwtOptions): IAuthenticationService
    {
        public async Task<ServiceResult<AuthResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return ServiceResult<AuthResponseDto>.Error(ServiceError.UnAuthorized, "Incorrect Email Or Password");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                return ServiceResult<AuthResponseDto>.Error(ServiceError.UnAuthorized, "Incorrect Email Or Password");

            var authDto =  new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Token = await JwtHelper.GenerateJwtTokenAsync(user, _userManager, _jwtOptions)
            };
            return ServiceResult<AuthResponseDto>.Ok(authDto);
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

            // cal user service to add the user 
          

            string token = await JwtHelper.GenerateJwtTokenAsync(applicationUser, _userManager, _jwtOptions);
           

            var authDto =  new AuthResponseDto
            {
                UserId = applicationUser.Id,
                Email = applicationUser.Email,
                Token = token
            };
            return ServiceResult<AuthResponseDto>.Ok(authDto);
        }

     
    }
}
