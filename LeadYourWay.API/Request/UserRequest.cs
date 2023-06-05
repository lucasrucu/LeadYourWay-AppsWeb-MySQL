using System.ComponentModel.DataAnnotations;

namespace LeadYourWay.API.Request;

public class UserRequest
{
    [Required]
    [MaxLength(60)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(60)]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(100)]
    [MinLength(5)]
    public string Password { get; set; }
    
    [Required]
    [MaxLength(15)]
    [MinLength(8)]
    public string Phone { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }
}