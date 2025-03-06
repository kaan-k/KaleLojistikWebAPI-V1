﻿using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Repositories;
using Entities.Concrete;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CreateUserManager : MongoRepository<TestClass>, ICreateUserService
    {
        public CreateUserManager(IMongoDatabase database)
        : base(database, "Test") 
        {
        }

    }
}
