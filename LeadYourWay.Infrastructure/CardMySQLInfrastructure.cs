using LeadYourWay.Infrastructure.Context;
using LeadYourWay.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace LeadYourWay.Infrastructure;

public class CardMySQLInfrastructure : ICardInfrastructure
{
    private LeadYourWayContext _context;

    public CardMySQLInfrastructure(LeadYourWayContext context)
    {
        _context = context;
    }

    public async Task<List<Card>> GetAllAsync()
    {
        return await _context.Cards.Where(c => c.IsActive).ToListAsync();
    }

    public List<Card> GetByUserId(int id)
    {
        return _context.Cards.Where(c => c.UserId == id && c.IsActive).ToList();
    }

    public Card GetById(int id)
    {
        try
        {
            return _context.Cards.FirstOrDefault(x => x.Id == id && x.IsActive);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public bool ExistsById(int id)
    {
        return _context.Cards.Any(e => e.Id == id && e.IsActive);
    }

    public bool save(Card value)
    {
        value.DateCreated = DateTime.Now;
        _context.Cards.Add(value);
        _context.SaveChanges();
        return true;
    }

    public bool update(int id, Card value)
    {
        value.Id = id;
        value.DateUpdated = DateTime.Now;
        _context.Cards.Update(value);
        _context.SaveChanges();
        return true;
    }

    public bool delete(int id)
    {
        var card = _context.Cards.Find(id);
        card.IsActive = false;
        _context.Cards.Update(card);
        _context.SaveChanges();
        return true;
    }
}