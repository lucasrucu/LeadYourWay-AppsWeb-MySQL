using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public class BicycleDomain : IBicycleDomain
{
    public IBicycleInfrastructure _bicycleInfrastructure;
    
    public BicycleDomain(IBicycleInfrastructure bicycleInfrastructure)
    {
        _bicycleInfrastructure = bicycleInfrastructure;
    }
    
    public bool save(Bicycle value)
    {
        // realizar validaciones aqui
        return _bicycleInfrastructure.save(value);
    }

    public bool update(int id, Bicycle value)
    {
        // realizar validaciones aqui
        return _bicycleInfrastructure.update(id, value);
    }

    public bool delete(int id)
    {
        return _bicycleInfrastructure.delete(id);
    }
}