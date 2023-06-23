using System.ComponentModel.DataAnnotations;

namespace LeadYourWay.API.Request;

public class CardRequest
{
    [Required] public string Name { get; set; }

    [Required] public string Number { get; set; }

    [Required] public DateTime ExpirationDate { get; set; }

    [Required] public string Cvv { get; set; }

    [Required] public string Type { get; set; }

    [Required] public int UserId { get; set; }
}