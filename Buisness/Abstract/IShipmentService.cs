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
        IResult Delete(string id);

        IDataResult<Employee> GetAssignedEmployee(string id);
        IDataResult<Shipment> GetById(string id);
        IDataResult<Shipment> GetBySenderId(string id);
        IDataResult<Shipment> GetByTrackingId(string id);
        IDataResult<List<Shipment>> GetAll();
    }
}
