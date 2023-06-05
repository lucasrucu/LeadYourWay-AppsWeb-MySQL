using LeadYourWay.Infrastructure;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public class UserDomain : IUserDomain
{
    public IUserInfrastructure _userInfrastructure;
    
    public UserDomain(IUserInfrastructure userInfrastructure)
    {
        _userInfrastructure = userInfrastructure;
    }
    
    public bool save(User value)
    {
        // realizar validaciones aqui
        return _userInfrastructure.save(value);
    }

    public bool update(int id, User value)
    {
        // realizar validaciones aqui
        return _userInfrastructure.update(id, value);
    }

    public bool delete(int id)
    {
        return _userInfrastructure.delete(id);
    }
}