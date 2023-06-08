using LeadYourWay.Infrastructure.Context;
using LeadYourWay.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace LeadYourWay.Infrastructure;

public class UserMySQLInfrastructure : IUserInfrastructure
{
    private LeadYourWayContext _leadYourWayContext;
    
    public UserMySQLInfrastructure(LeadYourWayContext leadYourWayContext)
    {
        _leadYourWayContext = leadYourWayContext;
    }
    
    public async Task<List<User>> GetAllAsync()
    {
        return await _leadYourWayContext.Users.Where(c => c.IsActive).ToListAsync();
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
        user.IsActive = false;
        _leadYourWayContext.Users.Update(user);
        _leadYourWayContext.SaveChanges();

        return true;
    }

    public bool ExistsByEmail(string email)
    {
        return _leadYourWayContext.Users.Any(e => e.Email == email);
    }
    
    public bool ExistsById(int id)
    {
        return _leadYourWayContext.Users.Any(e => e.Id == id);
    }
}