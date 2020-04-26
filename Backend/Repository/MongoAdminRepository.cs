using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public class MongoAdminRepository
    {
        MongoClient client;
        IMongoDatabase db;
        IMongoCollection<BsonDocument> collection;
        public MongoAdminRepository()
        {
            client = new MongoClient("mongodb://localhost:27017");
        }
        public string CreateDatabase(string dbName,string collectionName)
        {
            db = client.GetDatabase(dbName);
            CreateCollection(dbName,collectionName);
            return dbName;
        }

        public string GetDatabaseList()
        {
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            
            using (var cursor = client.ListDatabaseNames())
            {
                foreach (var document in cursor.ToEnumerable())
                {
                    Console.WriteLine(document.ToString());
                    arrayList.Add(document.ToString());
                    
                }
            }
            return arrayList.ToJson();

        }

        public void DropDatabase(string dbName)
        {
            client.DropDatabase(dbName);
        }

        public void CreateCollection(string dbName,string collectionName)
        {
            db = client.GetDatabase(dbName);
            var options = new CreateCollectionOptions { Capped = false};
            db.CreateCollection(collectionName, options);
        }

        public void DropCollection(string dbName, string collectionName)
        {
            db = client.GetDatabase(dbName);
            db.DropCollection(collectionName);
        }
    }
}
