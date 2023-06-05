using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadYourWay.Domain;
using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadYourWay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BicycleController : ControllerBase
    {
        // Injections
        private IBicycleInfrastructure _bicycleInfrastructure;
        private IBicycleDomain _bicycleDomain;
        
        public BicycleController(IBicycleInfrastructure bicycleInfrastructure, IBicycleDomain bicycleDomain)
        {
            _bicycleInfrastructure = bicycleInfrastructure;
            _bicycleDomain = bicycleDomain;
        }
        
        // GET: api/Bicycle
        [HttpGet (Name = "GetBicycle")]
        public List<Bicycle> Get()
        {
            return _bicycleInfrastructure.GetAll();
        }

        // GET: api/Bicycle/5
        [HttpGet("{id}", Name = "GetBicycleById")]
        public Bicycle Get(int id)
        {
            return _bicycleInfrastructure.GetById(id);
        }

        // POST: api/Bicycle
        [HttpPost (Name = "PostBicycle")]
        public void Post([FromBody] Bicycle value)
        {
            _bicycleDomain.save(value);
        }

        // PUT: api/Bicycle/5
        [HttpPut("{id}", Name = "PutBicycle")]
        public void Put(int id, [FromBody] Bicycle value)
        {
            _bicycleDomain.update(id, value);
        }

        // DELETE: api/Bicycle/5
        [HttpDelete("{id}", Name = "DeleteBicycle")]
        public void Delete(int id)
        {
            _bicycleDomain.delete(id);
        }
    }
}
