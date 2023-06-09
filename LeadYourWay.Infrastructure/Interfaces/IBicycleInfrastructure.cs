using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Infrastructure;

public interface IBicycleInfrastructure
{
    List<Bicycle> GetAll(); 
    List<Bicycle> GetByUserId(int id);
    Bicycle GetById(int id);
    public bool ExistsByID(int id);
    public bool save(Bicycle value);
    public bool update(int id, Bicycle value);
    public bool delete(int id);
}