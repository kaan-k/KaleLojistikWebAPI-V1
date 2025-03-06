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
        private readonly IMongoCollection<Shipment> _shipments;
        private readonly IEmployeeAssignmentService _employeeAssignmentService;


        public ShipmentManager(IShipmentDal shipmentDal, IEmployeeAssignmentService employeeAssignmentService, IMongoDatabase database)
        {
            _shipmentDal = shipmentDal;
            _employeeAssignmentService = employeeAssignmentService;
            _shipments = database.GetCollection<Shipment>("Shipments");
        }

        public IResult Add(Shipment shipment)
        {
            var assignedEmployee = _employeeAssignmentService.AssignEmployeeToShipment(shipment.WarehouseId);
            if (assignedEmployee == null)
            {
                return new ErrorResult("Bu depoda uygun çalışan bulunamadı.");
            }
            shipment.AssignedEmployeeId = assignedEmployee.Id;
            _shipments.InsertOne(shipment);
            //_shipmentDal.Add(shipment);
            return new SuccessResult("yay");
        }

        public IResult Delete(string id)
        {
            _shipments.DeleteOne(id);
            return new SuccessResult("yay");
        }

        public IDataResult<List<Shipment>> GetAll()
        {
            throw new NotImplementedException();
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

        public IResult Update(Shipment shipment, string id)
        {
            var filter = Builders<Shipment>.Filter.Eq(p => p.Id, id);
            var result = _shipments.ReplaceOne(filter, shipment);

            if (result.ModifiedCount > 0)
                return new SuccessResult("Shipment updated successfully!");
            else
                return new ErrorResult("No shipment was updated!");
        }
    }
}
