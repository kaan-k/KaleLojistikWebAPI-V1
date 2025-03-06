using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaleLojistikWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessUserController : ControllerBase
    {
        private readonly IBusinessUserService _businessUserInterface;

        public BusinessUserController(IBusinessUserService businessUserInterface)
        {
            _businessUserInterface = businessUserInterface;
        }


        [HttpPost("Add")]
        public IActionResult Add(BusinessUser businessUser)
        {
            var result = _businessUserInterface.Add(businessUser);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("Update")]
        public IActionResult Update(BusinessUser businessUser, string id)
        {
            var result = _businessUserInterface.Update(businessUser, id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("Delete")]
        public IActionResult Delete(string id)
        {
            var result = _businessUserInterface.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetById")]
        public IActionResult Get(string id)
        {
            var result = _businessUserInterface.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
