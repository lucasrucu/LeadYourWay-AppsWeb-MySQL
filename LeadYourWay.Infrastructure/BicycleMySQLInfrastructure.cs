using LeadYourWay.Infrastructure.Context;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Infrastructure;

public class BicycleMySQLInfrastructure : IBicycleInfrastructure
{
    private LeadYourWayContext _leadYourWayContext;
    
    public BicycleMySQLInfrastructure(LeadYourWayContext leadYourWayContext)
    {
        _leadYourWayContext = leadYourWayContext;
    }
    
    public List<Bicycle> GetAll()
    {
        return _leadYourWayContext.Bicycles.ToList();
    }

    public Bicycle GetById(int id)
    {
        return _leadYourWayContext.Bicycles.Find(id);
    }

    public bool save(Bicycle value)
    {
        _leadYourWayContext.Bicycles.Add(value);
        _leadYourWayContext.SaveChanges();
        
        return true;
    }

    public bool update(int id, Bicycle value)
    {
        Bicycle bicycle = value;
        bicycle.Id = id;
        
        _leadYourWayContext.Bicycles.Update(bicycle);
        _leadYourWayContext.SaveChanges();
        
        return true;
    }

    public bool delete(int id)
    {
        Bicycle bicycle =  _leadYourWayContext.Bicycles.Find(id);
        
        _leadYourWayContext.Bicycles.Remove(bicycle);
        _leadYourWayContext.SaveChanges();
        
        return true;
    }
}