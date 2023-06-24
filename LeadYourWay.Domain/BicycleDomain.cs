using System.Text.Json;
using System.Text.Json.Serialization;
using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public class BicycleDomain : IBicycleDomain
{
    private IBicycleInfrastructure _bicycleInfrastructure;
    private IUserInfrastructure _userInfrastructure;
    private IRentInfrastructure _rentInfrastructure;

    public BicycleDomain(IBicycleInfrastructure bicycleInfrastructure, IUserInfrastructure userInfrastructure, IRentInfrastructure rentInfrastructure)
    {
        _bicycleInfrastructure = bicycleInfrastructure;
        _userInfrastructure = userInfrastructure;
        _rentInfrastructure = rentInfrastructure;
    }

    public List<Bicycle> GetAll()
    {
        return _bicycleInfrastructure.GetAll();
    }

    public List<Bicycle> GetByUserId(int id)
    {
        return _bicycleInfrastructure.GetByUserId(id);
    }

    public List<Bicycle> getAvailableBicycles(DateTime start, DateTime end)
    {
        List<Bicycle> availableBicycles = new();
        List<Bicycle> bicycles = _bicycleInfrastructure.GetAll();
        
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve // Preserve object references to handle cycles
        };
        
        foreach (Bicycle bike in bicycles)
        {
            bool isAvailable = true;
            List<Rent> rents = _rentInfrastructure.GetByBikeId(bike.Id);
            foreach (Rent rent in rents)
            {
                if (rent.StartDate <= start && start <= rent.EndDate) 
                    isAvailable = false;
                if (rent.StartDate <= end && end <= rent.EndDate)
                    isAvailable = false;
                if (start <= rent.StartDate && rent.StartDate <= end) 
                    isAvailable = false;
                if (start <= rent.EndDate && rent.EndDate <= end)
                    isAvailable = false;
            }
            
            if (isAvailable)
                availableBicycles.Add(bike);
        }
        string serializedBicycles = JsonSerializer.Serialize(availableBicycles, options);

        return availableBicycles;
    }

    public Bicycle GetById(int id)
    {
        ExistsById(id);
        return _bicycleInfrastructure.GetById(id);
    }

    public bool save(Bicycle value)
    {
        IsValidSave(value);
        ExistsByUserId(value.UserId);
        return _bicycleInfrastructure.save(value);
    }

    public bool update(int id, Bicycle value)
    {
        ExistsById(id);
        return _bicycleInfrastructure.update(id, value);
    }

    public bool delete(int id)
    {
        ExistsById(id);
        return _bicycleInfrastructure.delete(id);
    }

    private static void IsValidSave(Bicycle bicycle)
    {
        if (bicycle.Name.Length > 50) throw new Exception("Bicycle name is too long");
        if (bicycle.Description.Length > 200) throw new Exception("Bicycle description is too long");
        if (bicycle.Price < 0) throw new Exception("Bicycle price is invalid");
        if (bicycle.UserId < 0) throw new Exception("Bicycle user id is invalid");
    }

    private void ExistsById(int id)
    {
        if (!_bicycleInfrastructure.ExistsById(id)) throw new Exception("Bicycle not found");
    }

    private void ExistsByUserId(int id)
    {
        if (!_userInfrastructure.ExistsById(id)) throw new Exception("User not found");
    }
}