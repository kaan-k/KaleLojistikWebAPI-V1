using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface IWarehouseService
    {
        IResult Add(Warehouse warehouse);
        IResult Update(Warehouse warehouse, string id);
        IResult Delete(string id);

        IDataResult<Warehouse> GetById(string id);
        IDataResult<List<Warehouse>> GetAll();
    }
}
