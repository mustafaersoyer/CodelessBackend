using Backend.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataController : Controller
    {
        BaseMongoRepository baseMongoRepository;

        void Init(string username, string collectionName)
        {
            baseMongoRepository = new BaseMongoRepository("denemeDB", "characters");
        }

        [HttpGet("{id}")]
        public virtual ActionResult GetModel(string id)
        {
            return Ok(this.baseMongoRepository.GetById(id));
        }

        [HttpGet]
        public string GetModelList(string username, string collectionName)
        {
            Init(username, collectionName);
            return this.baseMongoRepository.GetList();
        }

        [HttpGet]
        public string GetModelListEqual(string username, string collectionName,string colon,string value)
        {
            Init(username, collectionName);
            return this.baseMongoRepository.GetList(colon,value);
        }

        [HttpGet]
        public string GetModelListNotEqual(string username, string collectionName, string colon, string value)
        {
            Init(username, collectionName);
            return this.baseMongoRepository.GetListNotEqual(colon, value);
        }

        [HttpPost]
        public string GetModelListEqualMany(string username, string collectionName, string colon, [FromBody]dynamic[] value)
        {
            Init(username, collectionName);
            return this.baseMongoRepository.GetList(colon, value);
        }

        [HttpPost]
        public string GetModelListNotEqualMany(string username, string collectionName, string colon, [FromBody]dynamic[] value)
        {
            Init(username, collectionName);
            return this.baseMongoRepository.GetList(colon, value);
        }

        [HttpGet]
        public string GetModelListGt(string username, string collectionName, string colon, int value)
        {
            Init(username, collectionName);
            return this.baseMongoRepository.GetListGt(colon, value);
        }
        [HttpGet]
        public string GetModelListGte(string username, string collectionName, string colon, int value)
        {
            Init(username, collectionName);
            return this.baseMongoRepository.GetListGte(colon, value);
        }

        [HttpGet]
        public string GetModelListLt(string username, string collectionName, string colon, int value)
        {
            Init(username, collectionName);
            return this.baseMongoRepository.GetListLt(colon, value);
        }

        [HttpGet]
        public string GetModelListLte(string username, string collectionName, string colon, int value)
        {
            Init(username, collectionName);
            return this.baseMongoRepository.GetListLte(colon, value);
        }

        [HttpPost]
        public virtual ActionResult AddModel([FromBody]string model, string username, string collectionName)
        {
            Init(username, collectionName);

            /*MongoDB.Bson.BsonDocument document
                = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(model);*/
            BsonDocument doc = BsonDocument.Parse(model);
            
            return Ok(this.baseMongoRepository.Create(doc));
            
        }

        [HttpPut]
        public ActionResult UpdateModel(string id, [FromBody]string model, string username, string collectionName)
        {
            Init(username, collectionName);
            BsonDocument doc = BsonDocument.Parse(model);
            // MongoDB.Bson.BsonDocument document
            //  = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(model);
            return Ok(this.baseMongoRepository.Update(id, doc));
            
        }

        [HttpDelete]
        public virtual void DeleteModel(string id, string username, string collectionName)
        {
            Init(username, collectionName);
            this.baseMongoRepository.Delete(id);
        }
    }
}