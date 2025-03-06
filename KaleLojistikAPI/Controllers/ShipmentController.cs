using Buisness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KaleLojistikWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpPost("Add")]
        public IActionResult Add(Shipment shipment)
        {
            var result = _shipmentService.Add(shipment);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("Update")]
        public IActionResult Update(Shipment shipment, string id)
        {
            var result = _shipmentService.Update(shipment, id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("UpdateStatus")]
        public IActionResult UpdateStatus(string id, string newStatus)
        {
            var result = _shipmentService.UpdateStatus(id, newStatus);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("Delete")]
        public IActionResult Delete(string id)
        {
            var result = _shipmentService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetById")]
        public IActionResult GetById(string id)
        {
            var result = _shipmentService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetShipmentStatus")]
        public IActionResult GetShipmentStatus(string id)
        {
            var result = _shipmentService.GetShipmentStatusHistory(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetEmployeeByShipmentId")]
        public IActionResult GetEmployee(string id)
        {
            var result = _shipmentService.GetAssignedEmployee(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetBySenderId")]
        public IActionResult GetBySender(string id)
        {
            var result = _shipmentService.GetBySenderId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetByTrackingId")]
        public IActionResult GetByTrackingId(string id)
        {
            var result = _shipmentService.GetByTrackingId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
