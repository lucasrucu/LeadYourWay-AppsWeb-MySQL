using LeadYourWay.Infrastructure.Context;
using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Infrastructure;

public class RentMySQLInfrastructure : IRentInfrastructure
{
    private LeadYourWayContext _context;

    public RentMySQLInfrastructure(LeadYourWayContext context)
    {
        _context = context;
    }

    public bool save(Rent rent)
    {
        _context.Rents.Add(rent);
        _context.SaveChanges();
        return true;
    }

    public List<Rent> GetByBikeId(int bikeId)
    {
        return _context.Rents.Where(x => x.BicycleId == bikeId).ToList();
    }
}