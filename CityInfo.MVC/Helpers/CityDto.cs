﻿namespace CityInfo.MVC.Helpers;

public class CityDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public int Population { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public List<PointOfInterestDto>? PointOfInterests { get; set; }
    public List<LinkDto>? Links { get; set; }
}