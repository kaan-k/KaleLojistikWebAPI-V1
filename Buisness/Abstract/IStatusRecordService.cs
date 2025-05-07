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
    public interface IStatusRecordService
    {
        IResult Add(StatusRecord statusRecord);
        IDataResult<List<StatusRecord>> GetAll();
        IDataResult<List<StatusRecord>> GetByShipmentId(string id);
        IDataResult<StatusRecord> GetById(string id);
    }
}
