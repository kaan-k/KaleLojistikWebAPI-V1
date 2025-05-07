using Buisness.Abstract;
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
    public class SenderManager : ISenderService
    {
        private readonly IMongoCollection<Sender> _senders;

        public SenderManager(IMongoDatabase database)
        {
            _senders = database.GetCollection<Sender>("Senders");
        }

        public IResult Add(Sender sender)
        {
            _senders.InsertOne(sender);
            return new SuccessResult("Sender created successfully.");
        }

        public IResult Delete(string id)
        {
            _senders.DeleteOne(p => p.Id == id);
            return new SuccessResult("Sender deleted successfully.");
        }

        public IDataResult<List<Sender>> GetAll()
        {
            var senders = _senders.Find(_ => true).ToList();

            if (!senders.Any())
                return new ErrorDataResult<List<Sender>>("No senders found.");

            return new SuccessDataResult<List<Sender>>(senders, "Senders retrieved successfully.");
        }

        public IDataResult<Sender> GetById(string id)
        {
            var sender = _senders.Find(p => p.Id == id).FirstOrDefault();
            return new SuccessDataResult<Sender>(sender, "Sender found.");
        }

        public IResult Update(Sender sender, string id)
        {
            var filter = Builders<Sender>.Filter.Eq(p => p.Id, id);
            var result = _senders.ReplaceOne(filter, sender);

            if (result.ModifiedCount > 0)
                return new SuccessResult("Sender updated successfully!");
            else
                return new ErrorResult("No sender was updated!");
        }
    }
}
