using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public interface IUserDomain
{
    public bool save(User value);
    public bool update(int id, User value);
    public bool delete(int id);
}