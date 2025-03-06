using Buisness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaleLojistikWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityservice;

        public CityController(ICityService cityservice) {
            _cityservice = cityservice;
            }
        [HttpPost("Add")]
        public IActionResult Add(City city)
        {
            var result = _cityservice.Add(city);
            return Ok(result);
        }
    }
}
