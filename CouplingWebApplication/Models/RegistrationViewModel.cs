using System.ComponentModel.DataAnnotations;

namespace CouplingWebApplication.Models;

public class RegistrationViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    public string Adress { get; set; }
    
    [Required]
    [Display(Name = "Address 2")]
    public string Adress2 { get; set; }
    
    [Required]
    public string City { get; set; }
    
    [Required]
    public string State { get; set; }
    
    [Required]
    public string Zip { get; set; }
    
    [Display(Name = "Check me out")]
    public bool CheckMeOut { get; set; }
}