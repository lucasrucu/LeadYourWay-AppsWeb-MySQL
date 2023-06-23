using System.ComponentModel.DataAnnotations;

namespace LeadYourWay.API.Request;

public class BicycleRequest
{
    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }

    [Required] public double Price { get; set; }

    [Required] public string Size { get; set; }

    [Required] public int UserId { get; set; }

    public string Model { get; set; }

    public string Image { get; set; }
}