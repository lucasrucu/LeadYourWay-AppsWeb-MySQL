namespace LeadYourWay.Infrastructure.Models;

public class User : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public string Image { get; set; }
    public string Roles { get; set; }
    public DateTime BirthDate { get; set; }
    public virtual List<Bicycle> Bicycles { get; set; }
    public virtual List<Card> Cards { get; set; }
}