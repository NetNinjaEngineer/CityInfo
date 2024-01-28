using CityInfo.API.Models;

namespace CityInfo.API;

public static class SeedData
{
    public static IEnumerable<City> GetCities()
    {
        return
        [
            new(1, "New York", "USA", 8398748, 40.7128, -74.0060),
            new(2, "London", "UK", 8982000, 51.5099, -0.1180),
            new(3, "Tokyo", "Japan", 13929286, 35.6895, 139.6917),
             new() { Id = 4, Name = "Beijing", Country = "China", Population = 21516000, Latitude = 39.9042, Longitude = 116.4074 },
            new() { Id = 5, Name = "Paris", Country = "France", Population = 2140526, Latitude = 48.8566, Longitude = 2.3522 },
            new() { Id = 6, Name = "Delhi", Country = "India", Population = 30290936, Latitude = 28.6139, Longitude = 77.2090 },
            new() { Id = 7, Name = "Sao Paulo", Country = "Brazil", Population = 2200281, Latitude = -23.5505, Longitude = -46.6333 },
            new() { Id = 8, Name = "Istanbul", Country = "Turkey", Population = 15462435, Latitude = 41.0082, Longitude = 28.9784 },
            new() { Id = 9, Name = "Lagos", Country = "Nigeria", Population = 14497000, Latitude = 6.5244, Longitude = 3.3792 },
            new() { Id = 10, Name = "Moscow", Country = "Russia", Population = 12615882, Latitude = 55.7558, Longitude = 37.6176 },
            new() { Id = 11, Name = "Mumbai", Country = "India", Population = 12691836, Latitude = 19.0760, Longitude = 72.8777 },
            new() { Id = 12, Name = "Cairo", Country = "Egypt", Population = 20484965, Latitude = 30.0444, Longitude = 31.2357 },
            new() { Id = 13, Name = "Mexico City", Country = "Mexico", Population = 9209944, Latitude = 19.4326, Longitude = -99.1332 },
            new() { Id = 14, Name = "Bangkok", Country = "Thailand", Population = 8280925, Latitude = 13.7563, Longitude = 100.5018 },
            new() { Id = 15, Name = "Seoul", Country = "South Korea", Population = 9720846, Latitude = 37.5665, Longitude = 126.9780 },
        ];
    }

    public static IEnumerable<PointOfInterest> GetPointsOfInterests()
    {
        return
        [
            new (1, "Central Park", "Park", "A large urban park in Manhattan.", 40.7851, -73.9683, 1),
            new (2, "Empire State Building", "Landmark", "Iconic skyscraper in Midtown Manhattan.", 40.7484, -73.9857, 1),
            new (3, "Hyde Park", "Park", "One of the largest parks in London.", 51.5074, -0.1657, 2),
            new (4, "British Museum", "Museum", "World-famous museum of art and antiquities.", 51.5194, -0.1270, 2),
            new (5, "Ueno Park", "Park", "Famous public park in central Tokyo.", 35.7146, 139.7732, 3),
            new (6, "Tokyo Tower", "Landmark", "Iconic communications and observation tower.", 35.6586, 139.7454, 3),
            new (7, "Central Park", "Park", "A large urban park in Manhattan.", 40.7851, -73.9683, 4),
            new (8, "Empire State Building", "Landmark", "Iconic skyscraper in Midtown Manhattan.", 40.7484, -73.9857, 4),
            new (9, "Hyde Park", "Park", "One of the largest parks in London.", 51.5074, -0.1657, 5),
            new (10, "British Museum", "Museum", "World-famous museum of art and antiquities.", 51.5194, -0.1270, 5),
            new (11, "Ueno Park", "Park", "Famous public park in central Tokyo.", 35.7146, 139.7732, 6),
            new (12, "Tokyo Tower", "Landmark", "Iconic communications and observation tower.", 35.6586, 139.7454, 5),
            new (13, "Central Park", "Park", "A large urban park in Manhattan.", 40.7851, -73.9683, 7),
            new (14, "Empire State Building", "Landmark", "Iconic skyscraper in Midtown Manhattan.", 40.7484, -73.9857, 8),
            new (15, "Hyde Park", "Park", "One of the largest parks in London.", 51.5074, -0.1657, 9),
            new (16, "British Museum", "Museum", "World-famous museum of art and antiquities.", 51.5194, -0.1270, 10),
            new (17, "Ueno Park", "Park", "Famous public park in central Tokyo.", 35.7146, 139.7732, 11),
            new (18, "Tokyo Tower", "Landmark", "Iconic communications and observation tower.", 35.6586, 139.7454, 12),
            new (19, "Central Park", "Park", "A large urban park in Manhattan.", 40.7851, -73.9683, 13),
            new (20, "Empire State Building", "Landmark", "Iconic skyscraper in Midtown Manhattan.", 40.7484, -73.9857, 15),
            new (21, "Hyde Park", "Park", "One of the largest parks in London.", 51.5074, -0.1657, 13),
            new (22, "British Museum", "Museum", "World-famous museum of art and antiquities.", 51.5194, -0.1270, 13),
            new (23, "Ueno Park", "Park", "Famous public park in central Tokyo.", 35.7146, 139.7732, 10),
            new (24, "Tokyo Tower", "Landmark", "Iconic communications and observation tower.", 35.6586, 139.7454, 14),
        ];
    }
}
