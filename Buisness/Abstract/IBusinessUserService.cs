using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBusinessUserService
    {
        IResult Add(BusinessUser businessUser);
        IResult Update(BusinessUser businessUser, string id);
        IResult Delete(string id);

        IDataResult<BusinessUser>GetById(string id);
        IDataResult<List<BusinessUser>> GetAll();
    }
}
