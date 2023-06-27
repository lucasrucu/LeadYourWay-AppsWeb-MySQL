using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeadYourWay.API.Filter;
using LeadYourWay.API.Request;
using LeadYourWay.Domain;
using LeadYourWay.Infrastructure.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadYourWay.API.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/rent")]
    [ApiController]
    public class RentController : ControllerBase
    {
        // Injections
        private IRentDomain _rentDomain;
        private IMapper _mapper;
        
        public RentController(IRentDomain rentDomain, IMapper mapper)
        {
            _rentDomain = rentDomain;
            _mapper = mapper;
        }
        
        // GET: api/rent/available
        [HttpGet("available/{bikeId}", Name = "GetAvailableBicycles")]
        public bool GetAvailableBicycles(int bikeId, DateTime start, DateTime end)
        {
            return _rentDomain.AvailableBicycle(bikeId, start, end);
        }
        
        // POST: api/rent
        [HttpPost (Name = "PostRent")]
        public bool Post([FromBody] RentRequest rentDto)
        {
            var rent = _mapper.Map<RentRequest, Rent>(rentDto);
            return _rentDomain.Save(rent);
        }
    }
}
