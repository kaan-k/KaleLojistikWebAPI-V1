using Core.Configuration;
using DataAccess.Abstract;
using DataAccess.Repositories;
using Entities.Concrete;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class Mongo_ComplaintDal: MongoRepository<Complaint>, IComplaintDal
    {
        public Mongo_ComplaintDal(IMongoDatabase database, IOptions<MongoDbSettings> settings)
           : base(database, settings.Value.BusinessUsersCollectionName)
        {
        }
    }
}
