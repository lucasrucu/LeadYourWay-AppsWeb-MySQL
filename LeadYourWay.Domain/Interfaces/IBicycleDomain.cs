using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public interface IBicycleDomain
{
    List<Bicycle> GetAll();
    List<Bicycle> GetByUserId(int id);
    List<Bicycle> getAvailableBicycles(DateTime start, DateTime end);
    Bicycle GetById(int id);
    public bool save(Bicycle value);
    public bool update(int id, Bicycle value);
    public bool delete(int id);
}