using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LeadYourWay.API.Request;
using LeadYourWay.Domain;
using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LeadYourWay.API.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        // Injections
        private ICardInfrastructure _cardInfrastructure;
        private IUserInfrastructure _userInfrastructure;
        private ICardDomain _cardDomain;
        private IUserDomain _userDomain;
        private IMapper _mapper;
        
        public CardController(
            ICardInfrastructure cardInfrastructure, 
            IUserInfrastructure userInfrastructure, 
            ICardDomain cardDomain, 
            IUserDomain userDomain,
            IMapper mapper
            )
        {
            _cardInfrastructure = cardInfrastructure;
            _userInfrastructure = userInfrastructure;
            _cardDomain = cardDomain;
            _userDomain = userDomain;
            _mapper = mapper;
        }
        
        // GET: api/Card
        [HttpGet (Name = "GetCard")]
        public async Task<List<Card>> GetAsync()
        {
            var cards = await _cardInfrastructure.GetAllAsync();
            return cards;
        }

        // GET: api/Card/5
        [HttpGet("{id}", Name = "Get")]
        public Card Get(int id)
        {
            var card = _cardInfrastructure.GetById(id);
            return card;
        }
        
        // GET: api/Card/filterByUserId/5
        [HttpGet("filterByUserId/{id}", Name = "GetByUserId")]
        public List<Card> GetByUserId(int id)
        {
            var cards = _cardInfrastructure.GetByUserId(id);
            return cards;
        }

        // POST: api/Card
        [HttpPost (Name = "PostCard")]
        public void Post([FromBody] CardRequest value)
        {
            if (ModelState.IsValid)
            {
                var card = _mapper.Map<CardRequest, Card>(value);
                _cardDomain.save(card);
            }
            else
            {
                StatusCode(400);
            }
        }

        // PUT: api/Card/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Card value)
        {
            _cardDomain.update(id, value);
        }

        // DELETE: api/Card/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _cardDomain.delete(id);
        }
    }
}
