using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadYourWay.API.Request;
using LeadYourWay.API.Response;
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
        public UserResponse Get(int id)
        {
            User user = _userInfrastructure.GetById(id);
            UserResponse userResponse = new UserResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                BirthDate = user.BirthDate
            };
            return userResponse;
        }

        // POST: api/User
        [HttpPost (Name = "PostUser")]
        public void Post([FromBody] User value)
        {
            if (ModelState.IsValid)
            {
                UserRequest userRequest = new UserRequest()
                {
                    Name = value.Name,
                    Email = value.Email,
                    Password = value.Password,
                    Phone = value.Phone,
                    BirthDate = value.BirthDate
                };
                _userDomain.save(value);
            }
            else
            {
                StatusCode(400);
            }
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
