using BlogService.Dtos.Blog;
using BlogService.Dtos.Result;
using BlogService.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController(IBlogService _blogService): ApiBaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetBlogsAsync()
        {
            var blogResult = await _blogService.GetBlogsAsync();
            return GetActionResult(blogResult);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogByIdAsync(Guid id)
        {
            var blogResult = await _blogService.GetBlogByIdAsync(id);
            return GetActionResult(blogResult);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlogAsync(BlogDto blogDto)
        {
            var updateBlogResult = await _blogService.UpdateBlogAsync(blogDto);
            return GetActionResult(updateBlogResult);
        }

        [HttpPost]
        public IActionResult CreateBlogAsync(CreateBlogDto createBlogDto)
        {
            var createBlogResult =  _blogService.CreateBlogAsync(createBlogDto);
            return GetActionResult(createBlogResult);
        }

        [HttpDelete]
        public async Task<IActionResult>  DeleteBlogAsync(Guid id)
        {
            var deleteBlogResult = await _blogService.DeleteBlogAsync(id, "");
            return GetActionResult(deleteBlogResult);
        }
    }
}
