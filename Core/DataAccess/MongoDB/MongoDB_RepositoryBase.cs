﻿using Core.Entities;
using Core.Entities.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.MongoDB
{
    public class MongoDB_RepositoryBase<TEntity, MongoDBContext> : IEntityRepository<TEntity>, IDisposable
         where TEntity : class, IEntity, new()
        where MongoDBContext : class, IMongoDB_Context<TEntity>, new()
    {
        public IMongoCollection<TEntity> _collection { get; set; }
        MongoDBContext mongoDBContext = new MongoDBContext();
        public IMongoDatabase _dataBase;
        public MongoDB_RepositoryBase()
        {
            _dataBase = mongoDBContext.database;

            _collection = mongoDBContext.GetMongoDBCollection();
        }
        public void Add(TEntity entity)
        {
            _collection.InsertOne(entity);
        }
        public DeleteResult Delete(string id)
        {
            return _collection.DeleteOne(e => e.Id == id);
        }
        public DeleteResult DeleteMany(Expression<Func<TEntity, bool>> filter = null)
        {
            return _collection.DeleteMany(filter);
        }
        public ReplaceOneResult Update(TEntity entity)
        {

            return _collection.ReplaceOne(e => e.Id == entity.Id, entity);

        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                return filter == null
            ? _collection.Find<TEntity>(document => true).SingleOrDefault()
            : _collection.Find<TEntity>(filter).SingleOrDefault();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                return filter == null
            ? _collection.Find<TEntity>(document => true).Sort("{_id:-1}").ToList()
            : _collection.Find<TEntity>(filter).Sort("{_id:-1}").ToList();

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public List<TEntity> GetAllWithPage(int page, int limit)
        {
            return _collection.Aggregate().Skip(page * limit).Limit(limit).ToList();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}
