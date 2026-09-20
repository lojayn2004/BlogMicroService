using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/dummy")]
    public class DummyController: ControllerBase    
    {

        [Authorize]
        [HttpGet]
        public string GetAsync()
        {
            return "Hello from DummyController";
        }
    }
}
