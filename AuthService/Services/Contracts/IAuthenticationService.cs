using AuthService.Dtos.Auth;
using AuthService.Dtos.Result;

namespace AuthService.Services.ServicesAbstractions
{
    public interface IAuthenticationService
    {
        public Task<ServiceResult<AuthResponseDto>> LoginAsync(LoginDto loginDto);

        public Task<ServiceResult<AuthResponseDto>> RegisterAsync(RegisterDto registerDto);
    }
}
