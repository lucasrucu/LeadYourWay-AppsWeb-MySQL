using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Infrastructure;

public interface IUserInfrastructure
{
    List<User> GetAll();
    User GetById(int id);
    public bool save(User value);
    public bool update(int id, User value);
    public bool delete(int id);
}