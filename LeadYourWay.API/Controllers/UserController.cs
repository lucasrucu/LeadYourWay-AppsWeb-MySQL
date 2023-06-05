using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadYourWay.Domain;
using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadYourWay.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Injections
        private IUserInfrastructure _userInfrastructure;
        private IUserDomain _userDomain;
        
        public UserController(IUserInfrastructure userInfrastructure, IUserDomain userDomain)
        {
            _userInfrastructure = userInfrastructure;
            _userDomain = userDomain;
        }
        
        // GET: api/User
        [HttpGet (Name = "GetUser")]
        public List<User> Get()
        {
            return _userInfrastructure.GetAll();
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUserById")]
        public User Get(int id)
        {
            return _userInfrastructure.GetById(id);
        }

        // POST: api/User
        [HttpPost (Name = "PostUser")]
        public void Post([FromBody] User value)
        {
            _userDomain.save(value);
        }

        // PUT: api/User/5
        [HttpPut("{id}", Name = "PutUser")]
        public void Put(int id, [FromBody] User value)
        {
            _userDomain.update(id, value);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}", Name = "DeleteUser")]
        public void Delete(int id)
        {
            _userDomain.delete(id);
        }
    }
}
