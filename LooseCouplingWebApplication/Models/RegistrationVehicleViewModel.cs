using System.ComponentModel.DataAnnotations;

namespace LooseCouplingWebApplication.Models;

public class RegistrationVehicleViewModel
{
    [Required]
    [Display(Name = "Identification Number")]
    public string IdentificationNumber { get; set; }
    
    [Required]
    public string Manufacturer { get; set; }
    
    [Required]
    public string Model { get; set; }
    
    [Required]
    public decimal Price { get; set; }
}