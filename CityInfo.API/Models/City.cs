namespace CityInfo.API.Models;

/// <summary>
/// City with id, name, country, population, latitude and longitude fields.
/// </summary>

public class City
{

    /// <summary>
    /// empty city constructor
    /// </summary>
    public City() { }

    /// <summary>
    /// Parameterize constructor with fields
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="country"></param>
    /// <param name="population"></param>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    public City(int id, string? name, string? country, int population, double latitude, double longitude)
    {
        Id = id;
        Name = name;
        Country = country;
        Population = population;
        Latitude = latitude;
        Longitude = longitude;
    }
    /// <summary>
    /// Id for city
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The Name of city
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Country of city
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Population of that city
    /// </summary>
    public int Population { get; set; }

    /// <summary>
    /// Latitude of that city
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Longitude of that city
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    /// PointOfInterests for this city
    /// </summary>
    public ICollection<PointOfInterest> PointOfInterests { get; set; } = new List<PointOfInterest>();
}
