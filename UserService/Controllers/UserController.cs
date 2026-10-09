using Microsoft.AspNetCore.Mvc;
using UsersService.Dtos.Users;
using UsersService.Services.Contracts;

namespace UsersService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController(IUserService _userService) : ApiBaseController
    {
        //[HttpPost]
        //public async Task<IActionResult> CreateUserAsync(CreateUserDto createUserDto)
        //{
        //    var createUserResult = await _userService.CreateAsync(createUserDto);
        //    return GetActionResult(createUserResult);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var getUserResult = await _userService.GetByIdAsync(id);
            return GetActionResult(getUserResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var deleteUserResult = await _userService.DeleteAsync(id);
            return GetActionResult(deleteUserResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(string id, UpdateUserDto updateUserDto)
        {
            var updateUserResult = await _userService.UpdateAsync(id, updateUserDto);
            return GetActionResult(updateUserResult);
        }
    }
}
