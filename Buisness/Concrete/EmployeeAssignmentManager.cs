using Buisness.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concrete
{
    public class EmployeeAssignmentManager : IEmployeeAssignmentService
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeAssignmentManager(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public Employee AssignEmployeeToShipment(string warehouseId)
        {
            var employees = _employeeService.GetAll().Data.Where(e => e.WarehouseId == warehouseId && e.Role.Contains("Lojistik")).ToList();

            if (!employees.Any())
            {
                return null;
            }
            return employees[new Random().Next(employees.Count)];
        }

        public Employee FetchAssignedEmployee(string shipmentId, IShipmentService shipmentService)
        {
            var shipment = shipmentService.GetById(shipmentId);
            if (shipment == null)
            {
                return null;
            }

            var assignedEmployeeId = shipment.Data.AssignedEmployeeId;
            if (string.IsNullOrEmpty(assignedEmployeeId))
            {
                return null;
            }

            // Çalışanı AssignedEmployeeId ile direkt sorgulayıp buluyoruz
            var returnEmp = _employeeService.GetAll()
                .Data
                .FirstOrDefault(e => e.Id == assignedEmployeeId);

            return returnEmp;
        }


    }
}
