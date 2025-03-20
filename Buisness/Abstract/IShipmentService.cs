using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IShipmentService
    {
        IResult Add(Shipment shipment);
        IResult Update(Shipment shipment, string id);
        IResult UpdateStatus(string shipmentId, string newStatus);
        IResult Delete(string id);

        IResult ConfirmDelivery(string trackingNumber, string newStatus);

        IDataResult<List<StatusRecord>> GetShipmentStatusHistory(string id);

        IDataResult<Employee> GetAssignedEmployee(string id);
        IDataResult<Shipment> GetById(string id);
        IDataResult<Shipment> GetBySenderId(string id);
        IDataResult<BusinessUser> GetSenderId(string id);
        IDataResult<Shipment> GetByTrackingId(string id);
        IDataResult<List<Shipment>> GetAll();
    }
}
