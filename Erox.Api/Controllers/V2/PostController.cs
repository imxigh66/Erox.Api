
using Microsoft.AspNetCore.Mvc;

namespace Erox.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PostsController : Controller
    {
        
        [HttpGet]
        public IActionResult GetById(int id)
        {
            
            return Ok();
        }
    }

}
