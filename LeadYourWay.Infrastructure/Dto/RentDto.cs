namespace LeadYourWay.Infrastructure.Models;

public class RentDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double TotalPrice { get; set; }
    public int CardId { get; set; }
    public int BicycleId { get; set; }
}