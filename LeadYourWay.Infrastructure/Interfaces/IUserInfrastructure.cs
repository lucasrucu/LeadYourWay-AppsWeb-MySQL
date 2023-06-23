using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Infrastructure;

public interface IUserInfrastructure
{
    Task<List<User>> GetAllAsync();
    User GetById(int id);
    public int GetUserIdByEmailAndPassword(User user);
    public bool ExistsById(int id);
    public bool ExistsByIdAndEmail(int id, string email);
    public bool ExistsByEmail(string email);
    public bool ExistsByEmailAndPassword(string email, string password);
    public bool save(User user);
    public bool update(int id, UserDto user);
    public bool delete(int id);
    Task<User> GetByUsername(string username);
}