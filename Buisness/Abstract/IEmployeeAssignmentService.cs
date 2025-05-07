using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IEmployeeAssignmentService
    {
        Employee AssignEmployeeToShipment(string warehouseId);
        Employee FetchAssignedEmployee(string shipmentId, IShipmentService shipmentService);
    }
}
