using Buisness.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace KaleLojistikWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        private readonly ISenderService _senderService;
        private readonly IStatusRecordService _statusRecordService;
        public ShipmentController(IShipmentService shipmentService, IStatusRecordService statusRecordService, ISenderService enderService)
        {
            _shipmentService = shipmentService;
            _statusRecordService = statusRecordService;
            _senderService = enderService;
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

        [HttpPost("ConfirmDelivery")]
        public IActionResult ConfirmDelivery(string trackingNumber, string? newStatus)
        {
            newStatus = "Kargo teslim edildi.";
            var result = _shipmentService.ConfirmDelivery(trackingNumber, newStatus);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("UpdateStatus")]
        public IActionResult UpdateStatus(string id, string newStatus)
        {
            var resultt = _shipmentService.GetById(id);
            if (!resultt.Data.isDelivered)
            {
                var result = _shipmentService.UpdateStatus(id, newStatus);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            return BadRequest("Kargo teslim edilmiş!");
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
        [HttpGet("DeleteWithTrackingNumber")]
        public IActionResult DeleteWithTrackingNumber(string id)
        {
            var result = _shipmentService.DeleteWithTrackinNumber(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _shipmentService.GetAll();
            return Ok(result);
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
        [HttpGet("GetByShipmentId")]
        public IActionResult GetByShipmentId(string id)
        {
            var result = _statusRecordService.GetByShipmentId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAllStatus")]
        public IActionResult GetAllStatus()
        {
            var result = _statusRecordService.GetAll();
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
        [HttpGet("GetSenderById")]
        public IActionResult GetSenderId(string id)
        {
            var result = _shipmentService.GetSenderId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetByTrackingId")]
        public IActionResult GetByTrackingId(string id)
        {
            var shipmentResult = _shipmentService.GetByTrackingId(id);
            if (!shipmentResult.Success)
                return BadRequest();

            var shipment = shipmentResult.Data;

            var statusRecords = new List<StatusRecord>();
            foreach (var statusId in shipment.StatusRecordIds)
            {
                var statusResult = _statusRecordService.GetById(statusId);
                if (statusResult.Success)
                    statusRecords.Add(statusResult.Data);
            }

            var dto = new ShipmentDtoWithStatus
            {
                Id = shipment.Id,
                TrackingNumber = shipment.TrackingNumber,
                SenderId = shipment.SenderId,
                ReceiverName = shipment.ReceiverName,
                ReceiverPhone = shipment.ReceiverPhone,
                ReceiverEmail = shipment.ReceiverEmail,
                Weight = shipment.Weight,
                ShipmentType = shipment.ShipmentType,
                DeliveryAddress = shipment.DeliveryAddress,
                AssignedEmployeeId = shipment.AssignedEmployeeId,
                WarehouseId = shipment.WarehouseId,
                StatusRecords = statusRecords
            };

            return Ok(new { data = dto, message = "Shipment with statuses", success = true });
        }

        [HttpGet("GetShipment")]
        public IActionResult GetShipment()
        {
            var result = _shipmentService.GetShipment();
            return Ok(result);
        }
        [HttpGet("CalculatePrice")]
        public IActionResult CalculatePrice(decimal weight, string shipmentType)
        {
            decimal basePrice = 20;
            decimal pricePerKg = 5;
            decimal expressFee = shipmentType.ToLower() == "express" ? 15 : 0;

            decimal total = basePrice + (weight * pricePerKg) + expressFee;
            return Ok(new { price = total });
        }

        [HttpPost("AddWithOptionalSender")]
        public IActionResult AddWithOptionalSender([FromBody] ShipmentWithOptionalSenderDto dto)
        {
            if (dto.Sender != null)
            {
                dto.Sender.Id = ObjectId.GenerateNewId().ToString();
                var senderResult = _senderService.Add(dto.Sender);
                if (!senderResult.Success)
                    return BadRequest("Gönderici eklenemedi.");
                dto.Shipment.SenderId = dto.Sender.Id;
            }

            var shipmentResult = _shipmentService.Add(dto.Shipment);
            if (!shipmentResult.Success)
                return BadRequest("Kargo eklenemedi.");

            return Ok("Kargo oluşturuldu.");
        }
        [HttpGet("GetAllDeliveredShipments")]
        public IActionResult GetAllDeliveredShipments()
        {
            var result = _shipmentService.GetDeliveredShipments();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
