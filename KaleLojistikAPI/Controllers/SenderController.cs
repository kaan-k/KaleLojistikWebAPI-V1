using Buisness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaleLojistikWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly ISenderService _senderService;

        public SenderController(ISenderService senderService)
        {
            _senderService = senderService;
        }

        [HttpPost("Add")]
        public IActionResult Add(Sender sender)
        {
            var result = _senderService.Add(sender);
            if (result.Success)
                return Ok(result);
            return BadRequest();
        }

        [HttpPost("Update")]
        public IActionResult Update(Sender sender, string id)
        {
            var result = _senderService.Update(sender, id);
            if (result.Success)
                return Ok(result);
            return BadRequest();
        }

        [HttpGet("Delete")]
        public IActionResult Delete(string id)
        {
            var result = _senderService.Delete(id);
            if (result.Success)
                return Ok(result);
            return BadRequest();
        }

        [HttpGet("GetById")]
        public IActionResult GetById(string id)
        {
            var result = _senderService.GetById(id);
            if (result.Success)
                return Ok(result);
            return BadRequest();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _senderService.GetAll();
            if (result.Success)
                return Ok(result);
            return BadRequest();
        }
    }
}
