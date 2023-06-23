namespace LeadYourWay.Infrastructure.Models;

public class Rent : BaseModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double TotalPrice { get; set; }
    public int CardId { get; set; }
    public Card Card { get; set; }
    public int BicycleId { get; set; }
    public Bicycle Bicycle { get; set; }
}