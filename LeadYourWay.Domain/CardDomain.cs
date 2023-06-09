using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public class CardDomain : ICardDomain
{
    private ICardInfrastructure _cardInfrastructure;
    private IUserInfrastructure _userInfrastructure;
    
    public CardDomain(ICardInfrastructure cardInfrastructure, IUserInfrastructure userInfrastructure)
    {
        _cardInfrastructure = cardInfrastructure;
        _userInfrastructure = userInfrastructure;
    }

    public async Task<List<Card>> GetAllAsync()
    {
        return await _cardInfrastructure.GetAllAsync();
    }

    public List<Card> GetByUserId(int id)
    {
        return _cardInfrastructure.GetByUserId(id);
    }

    public Card GetById(int id)
    {
        return _cardInfrastructure.GetById(id);
    }

    public bool save(Card value)
    {
        IsValidSave(value);
        ExistsByUserId(value.UserId);
        return _cardInfrastructure.save(value);
    }
    
    public bool update(int id, Card value)
    {
        ExistsById(id);
        return _cardInfrastructure.update(id, value);
    }
    
    public bool delete(int id)
    {
        ExistsById(id);
        return _cardInfrastructure.delete(id);
    }

    private static void IsValidSave(Card card)
    {
        if (card.Name.Length == 0) throw new Exception("Name is required");
        if (card.Number.Length == 0) throw new Exception("Number is required");
        if (card.Cvv.Length == 0) throw new Exception("Cvv is required");
        if (card.ExpirationDate.ToString().Length == 0) throw new Exception("ExpirationDate is required");
        if (card.Type.Length == 0) throw new Exception("Type is required");
        if (card.Name.Length > 50) throw new Exception("Name has to be less than 50 characters");
        if (card.Number.Length > 50) throw new Exception("Number has to be less than 50 characters");
        if (card.Cvv.Length > 4) throw new Exception("Cvv has to be less than 4 characters");
        if (card.Type.Length > 50) throw new Exception("Type has to be less than 50 characters");
        if (card.ExpirationDate < DateTime.Now.AddDays(1)) throw new Exception("ExpirationDate has to be in the future");
        if (card.UserId < 0) throw new Exception("UserId is required");
    }
    
    private void ExistsById(int id)
    {
        if (!_cardInfrastructure.ExistsById(id)) throw new Exception("Card not found");
    }
    
    private void ExistsByUserId(int id)
    {
        if (!_userInfrastructure.ExistsById(id)) throw new Exception("User not fooo0und");
    }
}