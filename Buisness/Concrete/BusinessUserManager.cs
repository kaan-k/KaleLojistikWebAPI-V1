using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BusinessUserManager : IBusinessUserService
    {
        private readonly IBusinessUserDal _businessUserDal;
        public BusinessUserManager(IBusinessUserDal businessUserDal)
        {
            _businessUserDal = businessUserDal;
        }

        public IResult Add(BusinessUser buisnessUser)
        {
            _businessUserDal.Add(buisnessUser);
            return new SuccessResult("yay");
        }

        public IResult Delete(string id)
        {
            _businessUserDal.Delete(id);
            return new SuccessResult("yay");
        }

        public IDataResult<List<BusinessUser>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<BusinessUser> GetById(string id)
        {
            var user = _businessUserDal.Get(p => p.Id == id);
            if (user == null)
            {
                return new ErrorDataResult<BusinessUser>("User not found.");
            }
            return new SuccessDataResult<BusinessUser>(user, "User retrieved successfully.");
        }

        public IResult Update(BusinessUser buisnessUser, string id)
        {
            _businessUserDal.Update(buisnessUser);
            return new SuccessResult("yay");
        }
    }
}
