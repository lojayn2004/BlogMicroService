using Microsoft.AspNetCore.Mvc;
using AuthService.Dtos.Auth;
using AuthService.Services.ServicesAbstractions;


namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthenticationService _authService) : ApiBaseController
    {

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);


            if (!result.Success || result.Data is null)
                return GetActionResult(result); 

            SetRefreshTokenInCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiration);

            return Ok(new AuthResponseDto
            {
                Token = result.Data.Token,
                RefreshTokenExpiration = result.Data.RefreshTokenExpiration,
                UserId = result.Data.UserId,
                Email = result.Data.Email
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            return GetActionResult(result);
        }

       
        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
                return Unauthorized(new { message = "Invalid Token" });
            
            var result = await _authService.RefreshTokenAsync(refreshToken);
            if (!result.Success || result.Data is null)
                return GetActionResult(result);

            SetRefreshTokenInCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiration);
            return Ok(result.Data);
        }


        // logout 



        private void SetRefreshTokenInCookie(string refreshToken, DateTime expiresOn)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expiresOn.ToLocalTime()
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }


}
