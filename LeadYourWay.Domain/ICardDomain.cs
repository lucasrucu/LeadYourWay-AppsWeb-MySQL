using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public interface ICardDomain
{
    public bool save(Card value);
    public bool update(int id, Card value);
    public bool delete(int id);
}