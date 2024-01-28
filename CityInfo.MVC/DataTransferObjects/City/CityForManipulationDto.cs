using System.ComponentModel.DataAnnotations;

namespace CityInfo.MVC.DataTransferObjects.City;

public abstract class CityForManipulationDto
{
    [Required(ErrorMessage = "You should fill out this field")]
    [MaxLength(100, ErrorMessage = "Maximum length for name 100 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "You should fill out this field")]
    [MaxLength(100, ErrorMessage = "Maximum length for country 100 characters.")]
    public string? Country { get; set; }
    public int Population { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
