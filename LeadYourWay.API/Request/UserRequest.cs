using System.ComponentModel.DataAnnotations;

namespace LeadYourWay.API.Request;

public class UserRequest
{
    [MaxLength(60)] public string Name { get; set; }

    [MaxLength(60)] public string Email { get; set; }

    [MaxLength(100)] [MinLength(5)] public string Password { get; set; }

    [MaxLength(15)] [MinLength(8)] public string Phone { get; set; }

    public string Image { get; set; }

    public DateTime BirthDate { get; set; }
}