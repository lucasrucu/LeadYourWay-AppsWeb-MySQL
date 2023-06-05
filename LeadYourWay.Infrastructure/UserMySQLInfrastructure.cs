using LeadYourWay.Infrastructure.Context;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Infrastructure;

public class UserMySQLInfrastructure : IUserInfrastructure
{
    private LeadYourWayContext _leadYourWayContext;
    
    public UserMySQLInfrastructure(LeadYourWayContext leadYourWayContext)
    {
        _leadYourWayContext = leadYourWayContext;
    }
    
    public List<User> GetAll()
    {
        return _leadYourWayContext.Users.ToList();
    }

    public User GetById(int id)
    {
        return _leadYourWayContext.Users.Find(id);
    }

    public bool save(User value)
    {
        _leadYourWayContext.Users.Add(value);
        _leadYourWayContext.SaveChanges();

        return true;
    }

    public bool update(int id, User value)
    {
        User user = value;
        user.Id = id;
        
        _leadYourWayContext.Users.Update(user);
        _leadYourWayContext.SaveChanges();

        return true;
    }

    public bool delete(int id)
    {
        User user =  _leadYourWayContext.Users.Find(id);

        _leadYourWayContext.Users.Remove(user);
        _leadYourWayContext.SaveChanges();

        return true;
    }
}