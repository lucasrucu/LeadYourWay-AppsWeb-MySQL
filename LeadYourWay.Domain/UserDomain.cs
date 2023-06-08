using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public class UserDomain : IUserDomain
{
    private IUserInfrastructure _userInfrastructure;
    
    public UserDomain(IUserInfrastructure userInfrastructure)
    {
        _userInfrastructure = userInfrastructure;
    }
    
    public bool save(User value)
    {
        ExistsByEmail(value);
        IsValid(value);
        return _userInfrastructure.save(value);
    }

    public bool update(int id, User value)
    {
        ExistsByEmail(value);
        IsValid(value);
        value.Id = id;
        return _userInfrastructure.update(id, value);
    }

    public bool delete(int id)
    {
        ExistsById(id);
        return _userInfrastructure.delete(id);
    }

    private void IsValid(User user)
    {
        if (user.Name.Length == 0) throw new Exception("Name is required");
        if (user.Email.Length == 0) throw new Exception("Email is required");
        if (user.Password.Length == 0) throw new Exception("Password is required");
        if (user.Phone.Length == 0) throw new Exception("Phone is required");
        if (user.BirthDate.ToString().Length == 0) throw new Exception("BirthDate is required");
        if (user.Name.Length > 50) throw new Exception("Name has to be less than 50 characters");
        if (user.Email.Length > 50) throw new Exception("Email has to be less than 50 characters");
        if (user.Phone.Length > 15) throw new Exception("Phone has to be less than 15 characters");
        if (user.BirthDate > DateTime.Now.AddYears(-15)) throw new Exception("User has to be at least 15 years old");
    }
    
    private void ExistsByEmail(User user)
    {
        if (!_userInfrastructure.ExistsByEmail(user.Email)) throw new Exception("User with email: " + user.Email + ", already exists");
    }
    
    private void ExistsById(int id)
    {
        if (!_userInfrastructure.ExistsById(id)) throw new Exception("User with id: " + id + ", does not exist");
    }
}