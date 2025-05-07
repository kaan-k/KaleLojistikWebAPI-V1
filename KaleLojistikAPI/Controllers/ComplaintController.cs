using Buisness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaleLojistikWebAPI.Controllers
{
    [Route("api/complaints")]
    [ApiController]
    [EnableCors("AllowAngular")]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintService _complaintService;

        public ComplaintController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpPost("Add")]
        public IActionResult Add(Complaint complaint)
        {
            var result = _complaintService.Add(complaint);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("Update")]
        public IActionResult Update(Complaint complaint, string id)
        {
            var result = _complaintService.Update(complaint, id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("Respond")]
        public IActionResult Respond(Complaint complaint, string id)
        {
            var result = _complaintService.Update(complaint, id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("Delete")]
        public IActionResult Delete(string id)
        {
            var result = _complaintService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetById")]
        public IActionResult GetById(string id)
        {
            var result = _complaintService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetStatusById")]
        public IActionResult GetStatusById(string id)
        {
            var result = _complaintService.GetStatus(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = _complaintService.GetAll();
            if (result.Success)
                return Ok(result.Data);
            return BadRequest();
        }
    }
}
