using Core.Configuration;
using Core.DataAccess;
using Core.DataAccess.MongoDB;
using DataAccess.Abstract;
using DataAccess.Repositories;
using Entities.Concrete;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class Mongo_BusinessUserDal : MongoRepository<BusinessUser>, IBusinessUserDal
    {
        public Mongo_BusinessUserDal(IMongoDatabase database, IOptions<MongoDbSettings> settings)
            : base(database, settings.Value.BusinessUsersCollectionName)
        {
        }

        public List<BusinessUser> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
