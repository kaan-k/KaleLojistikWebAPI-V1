using Buisness.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concrete
{
    public class ComplaintManager : IComplaintService
    {
        private readonly IMongoCollection<Complaint> _complaints;
        public ComplaintManager(IMongoDatabase database)
        {
            _complaints = database.GetCollection<Complaint>("Complaints");
        }

        public IResult Add(Complaint complaint)
        {
            _complaints.InsertOne(complaint);
            return new SuccessResult("Complaint created successfully.");
        }

        public IResult Delete(string id)
        {
            //Bug
            _complaints.DeleteOne(p => p.Id == id);
            return new SuccessResult("Complaint deleted successfully.");
        }

        public IDataResult<List<Complaint>> GetAll()
        {
            var compliants = _complaints.Find(_ => true).ToList();

            if (!compliants.Any())
            {
                return new ErrorDataResult<List<Complaint>>("No compliants found.");
            }

            return new SuccessDataResult<List<Complaint>>(compliants, "compliants retrieved successfully.");

        }

        public IDataResult<Complaint> GetById(string id)
        {
            var result = _complaints.Find(p => p.Id == id).FirstOrDefault();

            return new SuccessDataResult<Complaint>(result, "Complaint found.");

        }

        public IDataResult<string> GetStatus(string id)
        {
            var complaint = _complaints.Find(p => p.Id == id).FirstOrDefault();
            return new SuccessDataResult<string>(complaint.Status, "Complaint found.");

        }

        public IResult Update(Complaint complaint, string id)
        {
            var filter = Builders<Complaint>.Filter.Eq(p => p.Id, id);
            var result = _complaints.ReplaceOne(filter, complaint);

            if (result.ModifiedCount > 0)
                return new SuccessResult("Complaint updated successfully!");
            else
                return new ErrorResult("No complaint was updated!");
        }

    }
}
