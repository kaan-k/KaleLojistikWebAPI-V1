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
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeDal _employeeDal;
        private readonly IWarehouseService _warehouseService;
        private readonly IMongoCollection<Employee> _employees;

        public EmployeeManager(IEmployeeDal employeeDal, IMongoDatabase database, IWarehouseService warehouseService)
        {
            _employeeDal = employeeDal;
            _employees = database.GetCollection<Employee>("Employees");
            _warehouseService = warehouseService;
        }
        public IResult Add(Employee employee)
        {
            _employees.InsertOne(employee);
            return new SuccessResult("31");
        }

        public IResult Delete(string id)
        {
            _employees.DeleteOne(id);
            return new SuccessResult("31");
        }

        public IDataResult<List<Employee>> GetAll()
        {
            var employees = _employees.Find(_ => true).ToList();

            if (!employees.Any())
            {
                return new ErrorDataResult<List<Employee>>("No employees found.");
            }

            return new SuccessDataResult<List<Employee>>(employees, "Employees retrieved successfully.");
        }

        public IDataResult<Employee> GetById(string id)
        {
            var employeeResult = _employees.Find(p => p.Id == id).SingleOrDefault();
            if (employeeResult == null)
            {
                return new ErrorDataResult<Employee>("Employee not found.");
            }
            return new SuccessDataResult<Employee>(employeeResult, "Employee found.");
        }

        public IDataResult<Warehouse> GetEmployeeWarehouse(string employeeId)
        {
            var employee = GetById(employeeId).Data;
            if (employee == null)
            {
                return new ErrorDataResult<Warehouse>("Çalışan bulunamadı.");
            }

            var warehouse = _warehouseService.GetById(employee.WarehouseId);
            return warehouse.Success
                ? new SuccessDataResult<Warehouse>(warehouse.Data, "Çalışanın bağlı olduğu depo getirildi.")
                : new ErrorDataResult<Warehouse>("Depo bulunamadı.");
        }

        public IResult Update(Employee employee, string id)
        {
            var filter = Builders<Employee>.Filter.Eq(p => p.Id, id);
            var result = _employees.ReplaceOne(filter, employee);

            if (result.ModifiedCount > 0)
                return new SuccessResult("Employee updated successfully!");
            else
                return new ErrorResult("No employee was updated!");
        }
    }
}
