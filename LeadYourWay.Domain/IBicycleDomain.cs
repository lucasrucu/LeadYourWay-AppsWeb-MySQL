using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public interface IBicycleDomain
{
    public bool save(Bicycle value);
    public bool update(int id, Bicycle value);
    public bool delete(int id);
}