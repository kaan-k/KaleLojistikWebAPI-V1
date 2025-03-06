using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Abstract
{
    public interface ICityService
    {
        IResult Add(City city);
        IResult Update(City city, string id);
        IResult Delete(string id);

        IDataResult<City> GetById(string id);
        IDataResult<List<City>> GetAll();
    }
}
