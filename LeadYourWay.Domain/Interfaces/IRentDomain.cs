using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public interface IRentDomain
{
    public bool Save(Rent rentDto);
    public bool AvailableBicycle(int id, DateTime start, DateTime end);
    public List<Bicycle> GetAvailableBicycles(DateTime start, DateTime end);
}