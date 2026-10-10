using BlogService.Dtos.Result;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogService.Controllers
{
    public class ApiBaseController: ControllerBase
    {
        
        protected IActionResult GetActionResult<T>(ServiceResult<T> result)
        {
            if (result.IsSuccess == true)
                return Ok(new { data = result.Data, success = true });

            if (result.ResultType == ResultType.UnAuthorized)
                return Unauthorized(new { message = result.Message, sucess = false });
            if (result.ResultType == ResultType.Validation)
                return BadRequest(new { message = result.Message, success = false });
            if (result.ResultType == ResultType.NotFound)
                return NotFound(new { message = result.Message, success = false });

            return Problem(detail: result.Message);

        }
    }
}
