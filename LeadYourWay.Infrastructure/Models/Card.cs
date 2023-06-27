using System.ComponentModel.DataAnnotations.Schema;

namespace LeadYourWay.Infrastructure.Models;

public class Card : BaseModel
{
    public string Name { get; set; }
    public string Number { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Cvv { get; set; }
    public string Type { get; set; }
    public int UserId { get; set; }
    //public virtual User User { get; set; } = null!;
    //public List<Rent> Rents { get; set; }
}