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
    public interface IComplaintService
    {
        IResult Add(Complaint complaint);
        IResult Update(Complaint complaint, string id);
        IDataResult<Complaint> GetById(string id);
        IDataResult<string> GetStatus(string id);
        IDataResult<List<Complaint>> GetAll();
        IResult Delete(string id);
    }
}
