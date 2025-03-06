using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaleLojistikWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        [HttpPost("Add")]
        public IActionResult Add()
        {
            return BadRequest();
        }
    }
}
