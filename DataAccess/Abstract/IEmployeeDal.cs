using Core.DataAccess.MongoDB;
using Core.Entities.Concrete;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IEmployeeDal : IMongoRepository<Employee>
    {

    }
}
