using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Infrastructure;

public interface IRentInfrastructure
{
    public bool save(Rent rent);
    
    List<Rent> GetByBikeId(int bikeId);
}