using Buisness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concrete
{
    public class CityManager : ICityService
    {
        private readonly ICityDal _cityDal;
        public CityManager(ICityDal citydal) {
            _cityDal = citydal;
        }
        public IResult Add(City city)
        {
            _cityDal.Add(city);
            return new SuccessResult("yay");
        }

        public IResult Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<City>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<City> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(City city, string id)
        {
            throw new NotImplementedException();
        }
    }
}
