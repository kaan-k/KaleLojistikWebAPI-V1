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
    public interface IEmployeeService
    {
        IResult Add(Employee employee);
        IResult Update(Employee employee, string id);
        IResult Delete(string id);
        IDataResult<Employee> GetById(string id);
        IDataResult<Warehouse> GetEmployeeWarehouse(string employeeId);
        IDataResult<List<Employee>> GetAll();
    }
}
