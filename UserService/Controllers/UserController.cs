using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UsersService.Dtos.Users;
using UsersService.Services.Contracts;

namespace UsersService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController(IUserService _userService) : ApiBaseController
    {
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var getUserResult = await _userService.GetByIdAsync(id);
            return GetActionResult(getUserResult);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != id)
                return Unauthorized();

            var deleteUserResult = await _userService.DeleteAsync(id);
            return GetActionResult(deleteUserResult);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(string id, UpdateUserDto updateUserDto)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != id)
                return Unauthorized();
            var updateUserResult = await _userService.UpdateAsync(id, updateUserDto);
            return GetActionResult(updateUserResult);
        }
    }
}
