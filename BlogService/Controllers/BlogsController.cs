using BlogService.Dtos.Blog;
using BlogService.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
 
    public class BlogsController(IBlogService _blogService): ApiBaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetBlogsAsync([FromQuery] BlogQuerySearch blogQuerySearch)
        {
            var blogResult = await _blogService.GetBlogsAsync(blogQuerySearch);
            return GetActionResult(blogResult);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogByIdAsync(Guid id)
        {
            var blogResult = await _blogService.GetBlogByIdAsync(id);
            return GetActionResult(blogResult);

        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateBlogAsync(BlogDto blogDto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId))
                return Unauthorized();
            
            var updateBlogResult = await _blogService.UpdateBlogAsync(userId, blogDto);
            return GetActionResult(updateBlogResult);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateBlogAsync(CreateBlogDto createBlogDto)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            Console.WriteLine($"UserId: {userId}, Title: {createBlogDto.Title}, Body: {createBlogDto.Body}");
            var createBlogResult =  _blogService.CreateBlogAsync(userId, createBlogDto);
            return GetActionResult(createBlogResult);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult>  DeleteBlogAsync(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var deleteBlogResult = await _blogService.DeleteBlogAsync(userId, id);
            return GetActionResult(deleteBlogResult);
        }
    }
}
