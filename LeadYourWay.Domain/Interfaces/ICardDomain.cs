using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public interface ICardDomain
{
    Task<List<Card>> GetAllAsync();
    List<Card> GetByUserId(int id);
    Card GetById(int id);
    public bool save(Card value);
    public bool update(int id, Card value);
    public bool delete(int id);
}