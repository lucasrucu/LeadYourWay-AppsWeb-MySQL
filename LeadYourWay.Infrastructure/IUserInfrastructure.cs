using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Infrastructure;

public interface IUserInfrastructure
{
    Task<List<User>> GetAllAsync();
    User GetById(int id);
    public bool save(User value);
    public bool update(int id, User value);
    public bool delete(int id);
    public bool ExistsByEmail(string email);
    public bool ExistsById(int id);
}