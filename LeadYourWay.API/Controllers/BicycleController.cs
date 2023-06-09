using AutoMapper;
using LeadYourWay.API.Request;
using LeadYourWay.Domain;
using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LeadYourWay.API.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BicycleController : ControllerBase
    {
        // Injections
        private IBicycleDomain _bicycleDomain;
        private IMapper _mapper;
        
        public BicycleController(
            IBicycleDomain bicycleDomain, 
            IMapper mapper
            )
        {
            _bicycleDomain = bicycleDomain;
            _mapper = mapper;
        }
        
        // GET: api/Bicycle
        [HttpGet (Name = "GetBicycle")]
        public List<Bicycle> Get()
        {
            return _bicycleDomain.GetAll();
        }

        // GET: api/Bicycle/5
        [HttpGet("{id}", Name = "GetBicycleById")]
        public Bicycle Get(int id)
        {
            return _bicycleDomain.GetById(id);
        }
        
        // GET: api/Bicycle/filterByUserId/5
        [HttpGet("filterByUserId/{id}", Name = "GetBicycleByUserId")]
        public List<Bicycle> GetByUserId(int id)
        {
            return _bicycleDomain.GetByUserId(id);
        }

        // POST: api/Bicycle
        [HttpPost (Name = "PostBicycle")]
        public void Post([FromBody] BicycleRequest value)
        {
            if (ModelState.IsValid)
            {
                var bicycle = _mapper.Map<BicycleRequest, Bicycle>(value);
                _bicycleDomain.save(bicycle);
            }
            else
            {
                StatusCode(400);
                throw new Exception("Data was invalid");
            }
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
