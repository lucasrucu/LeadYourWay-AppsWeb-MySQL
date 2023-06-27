using LeadYourWay.Domain.Model;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public interface IUserDomain
{
    Task<List<User>> GetAllAsync();
    User GetById(int id);
    public int Login(User user);
    public bool save(User value);
    public bool update(int id, UserDto value);
    public bool delete(int id);
    Task<User> GetByUsername(string username);
    Task<LoginResponse> LoginRev1(User user);
    Task<int> SignupRev1(User user);
}