namespace CityInfo.MVC.DataTransferObjects.PointOfInterest;

public abstract class PointOfInterestForManipulationDto
{
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
