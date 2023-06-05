using System.ComponentModel.DataAnnotations.Schema;

namespace LeadYourWay.Infrastructure.Models;

public class Bicycle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Size { get; set; }
    public string Model { get; set; }
    public string Image { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User User { get; set; }
}