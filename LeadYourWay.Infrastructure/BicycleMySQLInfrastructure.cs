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
        return _leadYourWayContext.Bicycles.Where(x=>x.IsActive).ToList();
    }

    public List<Bicycle> GetByUserId(int id)
    {
        return _leadYourWayContext.Bicycles.Where(x => x.UserId == id && x.IsActive).ToList();
    }

    public Bicycle GetById(int id)
    {
        try
        {
            return _leadYourWayContext.Bicycles.Find(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public bool ExistsByID(int id)
    {
        return _leadYourWayContext.Bicycles.Any(e => e.Id == id && e.IsActive);
    }

    public bool save(Bicycle value)
    {
        value.DateCreated = DateTime.Now;
        _leadYourWayContext.Bicycles.Add(value);
        _leadYourWayContext.SaveChanges();
        return true;
    }

    public bool update(int id, Bicycle value)
    {
        value.Id = id;
        value.DateUpdated = DateTime.Now;
        _leadYourWayContext.Bicycles.Update(value);
        _leadYourWayContext.SaveChanges();
        return true;
    }

    public bool delete(int id)
    {
        Bicycle bicycle =  _leadYourWayContext.Bicycles.Find(id);
        bicycle.IsActive = false;
        _leadYourWayContext.Bicycles.Update(bicycle);
        _leadYourWayContext.SaveChanges();
        return true;
    }
}