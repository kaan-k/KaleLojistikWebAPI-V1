using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Shipment:IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TrackingNumber { get; set; }
        public string SenderId{ get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverEmail { get; set; }
        public decimal Weight { get; set; }
        public string ShipmentType { get; set; }
        //public string PickupAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public List<string>? StatusRecordIds { get; set; } = new List<string>();
        public string? AssignedEmployeeId { get; set; }
        public string WarehouseId { get; set; }
    }
}
