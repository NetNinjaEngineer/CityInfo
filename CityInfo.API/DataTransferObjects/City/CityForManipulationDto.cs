using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.DataTransferObjects.City;

/// <summary>
/// City for manupulation like update, delete and create .
/// </summary>
//[CityForManipulation]
public abstract class CityForManipulationDto
{
    /// <summary>
    /// The name of city
    /// </summary>
    [Required(ErrorMessage = "You should fill out the name.")]
    [MaxLength(100, ErrorMessage = "The title shouldn't have more than 100 characters.")]
    public string? Name { get; set; }

    /// <summary>
    /// The country for city
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string? Country { get; set; }

    /// <summary>
    /// The population of city
    /// </summary>
    public int Population { get; set; }

    /// <summary>
    /// The latitude of city
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// The longitude of city
    /// </summary>
    public double Longitude { get; set; }
}
