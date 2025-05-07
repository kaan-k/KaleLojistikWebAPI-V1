using Core.Entities.Abstract;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Complaint : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Type { get; set; }
        public string ShipmentId { get; set; }
        public string ComplaintMessageContent { get; set; }
        public string Status {  get; set; } = "Beklemede";
        public string? Response {  get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    }
}
