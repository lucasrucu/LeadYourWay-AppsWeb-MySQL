using LeadYourWay.Infrastructure.Models;

namespace LeadYourWay.Domain;

public interface IRentDomain
{
    public bool save(Rent rentDto);
}