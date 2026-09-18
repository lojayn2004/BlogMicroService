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
            return GetActionResult(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            return GetActionResult(result);
        }

        // refresh


        // logout 
    }


}
