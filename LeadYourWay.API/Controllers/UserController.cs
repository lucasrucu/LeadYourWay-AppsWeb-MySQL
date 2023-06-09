using AutoMapper;
using LeadYourWay.API.Request;
using LeadYourWay.API.Response;
using LeadYourWay.Domain;
using LeadYourWay.Infrastructure.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LeadYourWay.API
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Injections
        private IUserDomain _userDomain;
        private IMapper _mapper;
        
        public UserController(IUserDomain userDomain, IMapper mapper)
        {
            _userDomain = userDomain;
            _mapper = mapper;
        }
        
        // GET: api/User
        [HttpGet (Name = "GetUser")]
        public async Task<List<UserResponse>> GetAsync()
        {
            var users = await _userDomain.GetAllAsync();
            return _mapper.Map<List<User>, List<UserResponse>>(users);
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUserById")]
        public User Get(int id)
        {
            var user = _userDomain.GetById(id);
            var userResponse = _mapper.Map<User, UserResponse>(_userDomain.GetById(id));
            return user;
        }
        
        // POST: api/User/Login
        [HttpPost("Login", Name = "LoginUser")]
        public int Login([FromBody] LoginRequest value)
        {
            var user = _mapper.Map<LoginRequest, User>(value);
            return _userDomain.Login(user);
        }

        // POST: api/User
        [HttpPost (Name = "PostUser")]
        public void Post([FromBody] UserRequest value)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<UserRequest, User>(value);
                _userDomain.save(user);
            }
            else
            {
                StatusCode(400);
                throw new Exception("Data was invalid");
            }
        }

        // PUT: api/User/5
        [HttpPut("{id}", Name = "PutUser")]
        public void Put(int id, [FromBody] UserUpdateModel value)
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
