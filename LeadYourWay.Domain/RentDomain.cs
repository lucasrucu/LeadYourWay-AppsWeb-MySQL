using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public class RentDomain : IRentDomain
{
    private IRentInfrastructure _rentInfrastructure;
    private IUserInfrastructure _userInfrastructure;
    private IBicycleInfrastructure _bicycleInfrastructure;
    private ICardInfrastructure _cardInfrastructure;

    public RentDomain(
        IRentInfrastructure rentInfrastructure,
        IUserInfrastructure userInfrastructure,
        IBicycleInfrastructure bicycleInfrastructure,
        ICardInfrastructure cardInfrastructure
    )
    {
        _rentInfrastructure = rentInfrastructure;
        _userInfrastructure = userInfrastructure;
        _bicycleInfrastructure = bicycleInfrastructure;
        _cardInfrastructure = cardInfrastructure;
    }

    public bool save(Rent rent)
    {
        ValidateRent(rent);
        return _rentInfrastructure.save(rent);
    }

    private void ExistUser(int id)
    {
        if (_userInfrastructure.ExistsById(id)) return;
        throw new Exception("The user doesnt exist");
    }

    private void ExistBike(int id)
    {
        if (_bicycleInfrastructure.ExistsById(id)) return;
        throw new Exception("The bike doesnt exist");
    }

    private void ExistCard(int id)
    {
        if (_cardInfrastructure.ExistsById(id)) return;
        throw new Exception("The card doesnt exist");
    }

    private void ValidateDate(DateTime StartDate, DateTime EndDate)
    {
        var tomorrow = DateTime.Now.Date.AddDays(1);
        if (StartDate > EndDate) throw new Exception("The start date must be before the end date");
        if (StartDate < tomorrow) throw new Exception("The start date at least tomorrow");
        if (StartDate == EndDate) throw new Exception("The start date must be different from the end date");
    }
    
    private void ValidatePrice(double price)
    {
        if (price <= 0) throw new Exception("The price must be greater than 0");
    }

    private void ValidateBikeAvailability(int bikeId, DateTime startDate, DateTime endDate)
    {
        var rents = _rentInfrastructure.GetByBikeId(bikeId);
        foreach (var rent in rents)
        {
            if (rent.StartDate <= startDate && startDate <= rent.EndDate)
                throw new Exception("The bike is not available in the selected date");
            if (rent.StartDate <= endDate && endDate <= rent.EndDate)
                throw new Exception("The bike is not available in the selected date");
            if (startDate <= rent.StartDate && rent.StartDate <= endDate)
                throw new Exception("The bike is not available in the selected date");
            if (startDate <= rent.EndDate && rent.EndDate <= endDate)
                throw new Exception("The bike is not available in the selected date");
        }
    }

    private void ValidateRent(Rent rent)
    {
        ExistBike(rent.BicycleId);
        ExistCard(rent.CardId);
        var bike = _bicycleInfrastructure.GetById(rent.BicycleId);
        var card = _cardInfrastructure.GetById(rent.CardId);

        var lenderId = bike.UserId;
        var borrowerId = card.UserId;
        ExistUser(lenderId);
        ExistUser(borrowerId);

        ValidateDate(rent.StartDate, rent.EndDate);
        ValidatePrice(rent.TotalPrice);
        ValidateBikeAvailability(rent.BicycleId, rent.StartDate, rent.EndDate);
    }
}