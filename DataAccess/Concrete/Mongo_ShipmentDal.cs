using Core.Configuration;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Repositories;
using Entities.Concrete;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class Mongo_ShipmentDal : MongoRepository<Shipment>, IShipmentDal
    {
        public Mongo_ShipmentDal(IMongoDatabase database, IOptions<MongoDbSettings> settings)
            : base(database, settings.Value.BusinessUsersCollectionName)
        {
        }

        public List<ShipmentDto> GetShipment()
        {
            var list = new List<ShipmentDto>();
            var data = base._collection.Database.GetCollection<Shipment>("Shipments")?.Find(k => true)?.ToList();
            foreach (var item in data)
            {
                var list2 = new List<StatusRecord>();
                foreach (var item2 in item.StatusRecordIds.ToList())
                {
                    var da = base._collection.Database.GetCollection<StatusRecord>("StatusRecords")?.Find(k => k.Id == item2)?.FirstOrDefault();
                    list2.Add(da);
                }
                var p = (new ShipmentDto
                {
                    Id = item.Id,
                    AssignedEmployeeId = item.AssignedEmployeeId,
                    DeliveryAddress = item.DeliveryAddress,
                    ReceiverEmail = item.ReceiverEmail,
                    ReceiverName = item.ReceiverName,
                    ReceiverPhone = item.ReceiverPhone,
                    SenderId = item.SenderId,
                    ShipmentType = item.ShipmentType,
                    StatusRecordIds = list2.ToList(),
                    TrackingNumber = item.TrackingNumber,
                    WarehouseId = item.WarehouseId,
                    Weight = item.Weight
                });
                list.Add(p);
            }
            return list.ToList();
        }
    }
}
