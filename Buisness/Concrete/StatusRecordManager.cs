using Buisness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MongoDB.Driver;
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
        private readonly IMongoCollection<StatusRecord> _statusRecord;
        public StatusRecordManager(IStatusRecordDal statusRecordDal, IMongoDatabase database)
        {
            _statusRecordDal = statusRecordDal;
            _statusRecord = database.GetCollection<StatusRecord>("StatusRecords");
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

        public IDataResult<List<StatusRecord>> GetAll()
        {
            var statusRecords = _statusRecord.Find(_ => true).ToList();

            if (!statusRecords.Any())
            {
                return new ErrorDataResult<List<StatusRecord>>("No status found.");
            }

            return new SuccessDataResult<List<StatusRecord>>(statusRecords, "status retrieved successfully.");
        }

        public IDataResult<StatusRecord> GetById(string id)
        {
            var statusRecord = _statusRecord.Find(w => w.Id == id).FirstOrDefault();
            if (statusRecord == null)
            {
                return new ErrorDataResult<StatusRecord>("Status bulunamadı.");
            }
            return new SuccessDataResult<StatusRecord>(statusRecord, "Status bulundu.");
        }

        public IDataResult<List<StatusRecord>> GetByShipmentId(string id)
        {
            var statusRecords = _statusRecord.Find(x => x.ShipmentId == id).ToList();

            if (!statusRecords.Any())
            {
                return new ErrorDataResult<List<StatusRecord>>("No status found.");
            }

            return new SuccessDataResult<List<StatusRecord>>(statusRecords, "status retrieved successfully.");
        }
    }
}
