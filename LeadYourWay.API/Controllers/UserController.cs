using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeadYourWay.API.Request;
using LeadYourWay.API.Response;
using LeadYourWay.Domain;
using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadYourWay.API
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Injections
        private IUserInfrastructure _userInfrastructure;
        private IUserDomain _userDomain;
        private IMapper _mapper;
        
        public UserController(IUserInfrastructure userInfrastructure, IUserDomain userDomain, IMapper mapper)
        {
            _userInfrastructure = userInfrastructure;
            _userDomain = userDomain;
            _mapper = mapper;
        }
        
        // GET: api/User
        [HttpGet (Name = "GetUser")]
        public async Task<List<UserResponse>> GetAsync()
        {
            var users = await _userInfrastructure.GetAllAsync();
            return _mapper.Map<List<User>, List<UserResponse>>(users);
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUserById")]
        public User Get(int id)
        {
            var user = _userInfrastructure.GetById(id);
            var userResponse = _mapper.Map<User, UserResponse>(_userInfrastructure.GetById(id));
            return user;
        }

        // POST: api/User
        [HttpPost (Name = "PostUser")]
        public void Post([FromBody] UserRequest value)
        {
            if (ModelState.IsValid)
            {
                var userRequest = _mapper.Map<UserRequest, User>(value);
                _userDomain.save(userRequest);
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
