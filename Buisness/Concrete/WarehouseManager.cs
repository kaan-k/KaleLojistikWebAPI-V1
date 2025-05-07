using Buisness.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concrete
{
    public class WarehouseManager : IWarehouseService
    {
        private readonly IMongoCollection<Warehouse> _collection;
        //private readonly IShipmentService _shipmentService;

        public WarehouseManager(IMongoDatabase database)
        {
            _collection = database.GetCollection<Warehouse>("Warehouses");

        }

        public IResult Add(Warehouse warehouse)
        {
            _collection.InsertOne(warehouse);
            return new SuccessResult("Warehouse created successfully.");
        }

        public IResult Update(Warehouse warehouse, string id)
        {
            var existingWarehouse = GetById(id);
            if (existingWarehouse == null)
            {
                return new ErrorResult("Depo bulunamadı.");
            }

            warehouse.Id = id; 
            _collection.ReplaceOne(w => w.Id == id, warehouse);
            return new SuccessResult("Depo başarıyla güncellendi.");
        }

        public IResult Delete(string id)
        {
            var result = _collection.DeleteOne(w => w.Id == id);
            if (result.DeletedCount > 0)
            {
                return new SuccessResult("Depo başarıyla silindi.");
            }
            return new ErrorResult("Depo bulunamadı.");
        }

        public IDataResult<Warehouse> GetById(string id)
        {
            var warehouse = _collection.Find(w => w.Id == id).FirstOrDefault();
            if (warehouse == null)
            {
                return new ErrorDataResult<Warehouse>("Depo bulunamadı.");
            }
            return new SuccessDataResult<Warehouse>(warehouse, "Depo bulundu.");
        }

        public IDataResult<List<Warehouse>> GetAll()
        {
            var warehouses = _collection.Find(_ => true).ToList();
            return new SuccessDataResult<List<Warehouse>>(warehouses, "Tüm depolar listelendi.");
        }

        public IDataResult<List<Shipment>> GetShipmentsByWarehouseId(string id)
        {
            return new ErrorDataResult<List<Shipment>>("hata");
            //var shipments = _shipmentService.GetByWarehouseId(id);
            //if(shipments.Data == null)
            //{
            //    return new ErrorDataResult<List<Shipment>>("hata");
            //}
            //return new SuccessDataResult<List<Shipment>>(shipments.Data, "başarı");
        }
    }
}
