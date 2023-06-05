using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Infrastructure;

public interface IBicycleInfrastructure
{
    List<Bicycle> GetAll();
    public Bicycle GetById(int id);
    public bool save(Bicycle value);
    public bool update(int id, Bicycle value);
    public bool delete(int id);
}