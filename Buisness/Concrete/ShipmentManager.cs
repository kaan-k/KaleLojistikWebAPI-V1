using Buisness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concrete
{
    public class ShipmentManager : IShipmentService
    {
        private readonly IShipmentDal _shipmentDal;
        private readonly IStatusRecordDal _statusRecordDal;
        private readonly IMongoCollection<BusinessUser> _businessUsers;
        private readonly IMongoCollection<Shipment> _shipments;
        private readonly IEmployeeAssignmentService _employeeAssignmentService;


        public ShipmentManager(IShipmentDal shipmentDal, IEmployeeAssignmentService employeeAssignmentService, IMongoDatabase database, IStatusRecordDal statusRecordDal)
        {
            _shipmentDal = shipmentDal;
            _employeeAssignmentService = employeeAssignmentService;
            _shipments = database.GetCollection<Shipment>("Shipments");
            _statusRecordDal = statusRecordDal;
            _businessUsers = database.GetCollection<BusinessUser>("Users");
        }

        public IResult Add(Shipment shipment)
        {
            shipment.Id = ObjectId.GenerateNewId().ToString();

            var assignedEmployee = _employeeAssignmentService.AssignEmployeeToShipment(shipment.WarehouseId);
            if (assignedEmployee == null)
            {
                return new ErrorResult("Bu depoda uygun çalışan bulunamadı.");
            }
            shipment.AssignedEmployeeId = assignedEmployee.Id;
            if (shipment.StatusRecordIds == null)
            {
                shipment.StatusRecordIds = new List<string>();
            }
            _shipments.InsertOne(shipment);


            var newStatusRecord = new StatusRecord
            {
                Id = ObjectId.GenerateNewId().ToString(),
                ShipmentId = shipment.Id,
                Status = "Sipariş oluşturuldu.",
                Timestamp = DateTime.UtcNow
            };

            _statusRecordDal.Add(newStatusRecord);
            shipment.StatusRecordIds.Add(newStatusRecord.Id);
            var filter = Builders<Shipment>.Filter.Eq(s => s.Id, shipment.Id);
            var update = Builders<Shipment>.Update.AddToSet(s => s.StatusRecordIds, newStatusRecord.Id);
            _shipments.UpdateOne(filter, update);

            return new SuccessResult("Shipment created successfully.");
        }

        public IResult ConfirmDelivery(string trackingNumber, string newStatus)
        {
            newStatus = "Kargo teslim edildi.";
            var delivery = _shipments.Find(p=>p.TrackingNumber == trackingNumber).FirstOrDefault();


             var newStatusRecord = new StatusRecord
             {
                 Id = ObjectId.GenerateNewId().ToString(),
                 ShipmentId = delivery.Id,
                 Status = newStatus,
                 Timestamp = DateTime.UtcNow
             };


            _statusRecordDal.Add(newStatusRecord);


            delivery.StatusRecordIds.Add(newStatusRecord.Id);

            var filter = Builders<Shipment>.Filter.Eq(s => s.Id, delivery.Id);
            var update = Builders<Shipment>.Update.Set(s => s.StatusRecordIds, delivery.StatusRecordIds);

            _shipments.UpdateOne(filter, update);
            return new SuccessDataResult<Shipment>(delivery, "Shipment delivered successfully.");


        }

        public IResult Delete(string id)
        {
            _shipments.DeleteOne(id);
            return new SuccessResult("yay");
        }

        public IDataResult<List<Shipment>> GetAll()
        {
            var shipment = _shipments.Find(_ => true).ToList();
            return new SuccessDataResult<List<Shipment>>(shipment, "Tüm depolar listelendi.");
        }

        public IDataResult<Employee> GetAssignedEmployee(string id)
        {
            var assignedEmployee = _employeeAssignmentService.FetchAssignedEmployee(id,this);
            if (assignedEmployee == null)
            {
                return new ErrorDataResult<Employee>("No employee found");
            }
            return new SuccessDataResult<Employee>(assignedEmployee, "Employee found.");
        }

        public IDataResult<Shipment> GetById(string id)
        {
            //var shipResult = _shipments.Find(Builders<Shipment>.Filter.Eq("_id", ObjectId.Parse(id))).FirstOrDefault();

            var shipResult =  _shipments.Find(p=>p.Id == id).SingleOrDefault();
            if (shipResult == null)
            {
                return new ErrorDataResult<Shipment>("User not found.");
            }
            return new SuccessDataResult<Shipment>(shipResult,"User found.");
        }

        public IDataResult<Shipment> GetBySenderId(string id)
        {
            var senderResult = _shipments.Find(p=>p.SenderId == id).FirstOrDefault();
            if (senderResult == null)
            {
                return new ErrorDataResult<Shipment>("User not found.");
            }
            return new SuccessDataResult<Shipment>(senderResult, "User not found.");
        }

        public IDataResult<Shipment> GetByTrackingId(string id)
        {
            var trackingResult = _shipments.Find(p=>p.TrackingNumber == id).FirstOrDefault();
            if(trackingResult == null)
            {
                return new ErrorDataResult<Shipment>("User not found.");

            }
            return new SuccessDataResult<Shipment>(trackingResult, "User not found.");

        }

        public IDataResult<BusinessUser> GetSenderId(string id)
        {
            var businessUser = _businessUsers.Find(p=>p.Id==id).FirstOrDefault();

            return new SuccessDataResult<BusinessUser>(businessUser, "Business user retrived successfully.");
        }

        public IDataResult<List<StatusRecord>> GetShipmentStatusHistory(string id)
        {
            var shipment = _shipments.Find(s => s.Id == id).FirstOrDefault();
            if (shipment == null)
            {
                return new ErrorDataResult<List<StatusRecord>>("Shipment not found.");
            }

            var statusRecords = _statusRecordDal.GetAll(sr => sr.ShipmentId == id);

            return new SuccessDataResult<List<StatusRecord>>(statusRecords, "Shipment history retrieved successfully.");
        }

        public IResult Update(Shipment shipment, string id)
        {
            var filter = Builders<Shipment>.Filter.Eq(p => p.Id, id);
            var result = _shipments.ReplaceOne(filter, shipment);

            if (result.ModifiedCount > 0)
                return new SuccessResult("Shipment updated successfully!");
            else
                return new ErrorResult("No shipment was updated!");
        }

        public IResult UpdateStatus(string shipmentId, string newStatus)
        {
            var shipment = _shipments.Find(p => p.Id == shipmentId).FirstOrDefault();
            if (shipment == null)
            {
                return new ErrorDataResult<Shipment>("Shipment not found.");
            }

            var newStatusRecord = new StatusRecord
            {
                Id = ObjectId.GenerateNewId().ToString(), 
                ShipmentId = shipment.Id,
                Status = newStatus,
                Timestamp = DateTime.UtcNow
            };


            _statusRecordDal.Add(newStatusRecord);


            shipment.StatusRecordIds.Add(newStatusRecord.Id);

            var filter = Builders<Shipment>.Filter.Eq(s => s.Id, shipment.Id);
            var update = Builders<Shipment>.Update.Set(s => s.StatusRecordIds, shipment.StatusRecordIds);

            _shipments.UpdateOne(filter, update);

            return new SuccessDataResult<Shipment>(shipment, "Shipment status updated successfully.");
        }
    }
}
