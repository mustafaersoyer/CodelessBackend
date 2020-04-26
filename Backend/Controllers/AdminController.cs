using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        MongoAdminRepository mongoAdminRepository;

        public AdminController()
        {
            mongoAdminRepository = new MongoAdminRepository();
        }

        // GET: api/Admin
        [HttpGet]
        public string GetDatabaseList()
        {
            return mongoAdminRepository.GetDatabaseList();
        }

        // GET: api/Admin/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Admin
        [HttpPost]
        public string CreateDatabase(string dbName,string collectionName)
        {
            return mongoAdminRepository.CreateDatabase(dbName, collectionName);
        }

        [HttpPost]
        public void CreateCollection(string dbName, string collectionName)
        {
            mongoAdminRepository.CreateCollection(dbName,collectionName);
        }

        // PUT: api/Admin/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public void DropDatabase(string dbName)
        {
            mongoAdminRepository.DropDatabase(dbName);
        }

        [HttpDelete]
        public void DropCollection(string dbName,string collectionName)
        {
            mongoAdminRepository.DropCollection(dbName,collectionName);
        }
    }
}
