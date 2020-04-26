using Microsoft.CodeAnalysis;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Model;
using System.Text.Json;
using System.Threading.Tasks;
using MongoDB.Bson.IO;
using Newtonsoft.Json;

namespace Backend.Model
{
    public class BaseMongoRepository
    {
        MongoClient _client;
        IMongoDatabase _db;
        IMongoCollection<BsonDocument> mongoCollection;

        public BaseMongoRepository(string dbName, string collection)
        {
             _client = new MongoClient("mongodb://localhost:27017");
             _db = _client.GetDatabase(dbName);
             mongoCollection = _db.GetCollection<BsonDocument>(collection);

        }

        public string GetList()
        {
            return mongoCollection.Find(new BsonDocument()).ToList().ToJson();
        }

        public string GetList(string colon,string value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(colon, value);
            return mongoCollection.Find(filter).ToList().ToJson();
        }

        public string GetList(string colon, dynamic[] value)
        {
            var filter = Builders<BsonDocument>.Filter.In(colon, value);
            return mongoCollection.Find(filter).ToList().ToJson();
        }

        public string GetListNotEqualMany(string colon, string value)
        {
            var filter = Builders<BsonDocument>.Filter.Nin(colon, value);
            return mongoCollection.Find(filter).ToList().ToJson();
        }

        public string GetListNotEqual(string colon, string value)
        {
            var filter = Builders<BsonDocument>.Filter.Ne(colon, value);
            return mongoCollection.Find(filter).ToList().ToJson();
        }

        public string GetListGt(string colon, int value)
        {
            var filter = Builders<BsonDocument>.Filter.Gt(colon, value);
            return mongoCollection.Find(filter).ToList().ToJson();
        }

        public string GetListGte(string colon, int value)
        {
            var filter = Builders<BsonDocument>.Filter.Gte(colon, value);
            return mongoCollection.Find(filter).ToList().ToJson();
        }

        public string GetListLt(string colon, int value)
        {
            var filter = Builders<BsonDocument>.Filter.Lt(colon, value);
            return mongoCollection.Find(filter).ToList().ToJson();
        }
        public string GetListLte(string colon, int value)
        {
            var filter = Builders<BsonDocument>.Filter.Lte(colon, value);
            return mongoCollection.Find(filter).ToList().ToJson();
        }



        public string GetById(string id)
        {
            var docId = new ObjectId(id);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", docId);
            var document = mongoCollection.Find(filter).First().ToJson();
            return document;
        }

        public string Create(BsonDocument model)
        {
            mongoCollection.InsertOne(model);
            return model.ToJson();
        }

        public string Update(string id, BsonDocument model)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            mongoCollection.ReplaceOne(filter, model);
            return model.ToJson();
        }

        public void Delete(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            mongoCollection.DeleteOne(filter);
        }
    }
}

