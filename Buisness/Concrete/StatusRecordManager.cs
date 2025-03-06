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
    public class StatusRecordManager : IStatusRecordService
    {
        private readonly IStatusRecordDal _statusRecordDal;

        public StatusRecordManager(IStatusRecordDal statusRecordDal)
        {
            _statusRecordDal = statusRecordDal;
        }
        public IResult Add(StatusRecord statusRecord)
        {
            if (statusRecord == null)
            {
                return new ErrorResult("Invalid status record.");
            }

            _statusRecordDal.Add(statusRecord);
            return new SuccessResult("Status record added successfully.");
        }

    }
}
