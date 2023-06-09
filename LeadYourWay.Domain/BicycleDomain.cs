using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public class BicycleDomain : IBicycleDomain
{
    private IBicycleInfrastructure _bicycleInfrastructure;
    private IUserInfrastructure _userInfrastructure;
    
    public BicycleDomain(IBicycleInfrastructure bicycleInfrastructure, IUserInfrastructure userInfrastructure)
    {
        _bicycleInfrastructure = bicycleInfrastructure;
        _userInfrastructure = userInfrastructure;
    }

    public List<Bicycle> GetAll()
    {
        return _bicycleInfrastructure.GetAll();
    }

    public List<Bicycle> GetByUserId(int id)
    {
        return _bicycleInfrastructure.GetByUserId(id);
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
        if (!_bicycleInfrastructure.ExistsByID(id)) throw new Exception("Bicycle not found");
    }
    
    private void ExistsByUserId(int id)
    {
        if (!_userInfrastructure.ExistsById(id)) throw new Exception("User not found");
    }
}