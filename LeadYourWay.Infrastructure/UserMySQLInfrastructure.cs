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
        try
        {
            return _leadYourWayContext.Users.FirstOrDefault(x => x.Id == id && x.IsActive);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public int GetUserIdByEmailAndPassword(User user)
    {
        var matchingUser = _leadYourWayContext.Users.FirstOrDefault(
            c => c.IsActive &&
                 c.Email == user.Email &&
                 c.Password == user.Password);
        return matchingUser.Id;
    }

    public bool ExistsById(int id)
    {
        return _leadYourWayContext.Users.Any(e => e.Id == id && e.IsActive);
    }

    public bool ExistsByIdAndEmail(int id, string email)
    {
        return _leadYourWayContext.Users.Any(
            e => e.Id == id &&
                 e.Email == email &&
                 e.IsActive == true);
    }

    public bool ExistsByEmail(string email)
    {
        return _leadYourWayContext.Users.Any(e => e.Email == email && e.IsActive == true);
    }

    public bool ExistsByEmailAndPassword(string email, string password)
    {
        return _leadYourWayContext.Users.Any(
            e => e.Email == email &&
                 e.Password == password &&
                 e.IsActive == true);
    }

    public bool save(User value)
    {
        value.DateCreated = DateTime.Now;
        _leadYourWayContext.Users.Add(value);
        _leadYourWayContext.SaveChanges();
        return true;
    }

    public bool update(int id, UserDto value)
    {
        var user = _leadYourWayContext.Users.Find(id);
        user.Name = value.Name;
        user.Email = value.Email;
        user.Password = value.Password;
        user.Phone = value.Phone;
        user.BirthDate = value.BirthDate;
        user.Image = value.Image;
        user.Id = id;
        user.DateUpdated = DateTime.Now;
        _leadYourWayContext.Users.Update(user);
        _leadYourWayContext.SaveChanges();
        return true;
    }

    public bool delete(int id)
    {
        var user = _leadYourWayContext.Users.Find(id);
        user.IsActive = false;
        _leadYourWayContext.Users.Update(user);
        _leadYourWayContext.SaveChanges();
        return true;
    }

    public async Task<User> GetByUsername(string username)
    {
        return await _leadYourWayContext.Users.SingleAsync(u => u.Email == username);
    }

    public async Task<int> Signup(User user)
    {
        user.DateCreated = DateTime.Now;
        user.Roles = "admin";
        _leadYourWayContext.Users.Add(user);
        await _leadYourWayContext.SaveChangesAsync();
        return user.Id;
    }
}