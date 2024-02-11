using CityInfo.API.Models;

namespace CityInfo.API
{
    public static class SeedData
    {
        public static IEnumerable<City> GetCities()
        {
            return
            [
                new() { Id = 1, Name = "New York",Country = "USA", Population = 8398748, Latitude = 40.7128, Longitude = -74.0060 },
                new() { Id = 2, Name = "London", Country = "UK", Population = 8982000, Latitude = 51.5099, Longitude = -0.1180 },
                new() { Id = 3, Name ="Tokyo", Country =  "Japan", Population = 13929286, Latitude = 35.6895, Longitude = 139.6917 },
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
            var cities = GetCities();

            return new List<PointOfInterest>
            {
                new() { Id = 1, Name = "Central Park", Category = "Park", Description = "A large urban park in Manhattan.", CityId = cities.ElementAt(0).Id },
                new() { Id = 2, Name = "Times Square", Category = "Landmark", Description = "Iconic commercial intersection and neighborhood in Midtown Manhattan.", CityId = cities.ElementAt(0).Id },
                new() { Id = 3, Name = "Buckingham Palace", Category = "Landmark", Description = "The London residence and administrative headquarters of the monarch of the United Kingdom.", CityId = cities.ElementAt(1).Id },
                new() { Id = 4, Name = "The British Museum", Category = "Museum", Description = "World-famous museum of art and antiquities.", CityId = cities.ElementAt(1).Id },
                new() { Id = 5, Name = "Ueno Park", Category = "Park", Description = "Famous public park in central Tokyo.", CityId = cities.ElementAt(2).Id },
                new() { Id = 6, Name = "Tokyo Tower", Category = "Landmark", Description = "Iconic communications and observation tower.", CityId = cities.ElementAt(2).Id },
                new() { Id = 7, Name = "Eiffel Tower", Category = "Landmark", Description = "Iconic iron lattice tower located on the Champ de Mars in Paris.",CityId = cities.ElementAt(4).Id },
                new() { Id = 8, Name = "Louvre Museum", Category = "Museum", Description = "The world's largest art museum and a historic monument in Paris.", CityId = cities.ElementAt(4).Id },
                new() { Id = 9, Name = "Red Square", Category = "Landmark", Description = "City square in Moscow, often considered the central square of Moscow and all of Russia.",CityId = cities.ElementAt(9).Id },
                new() { Id = 10, Name = "The Kremlin", Category = "Landmark", Description = "Historic fortified complex at the heart of Moscow, overlooking the Moskva River to the south.", CityId = cities.ElementAt(9).Id },
                new() { Id = 11, Name = "Delhi Gate", Category = "Landmark", Description = "Historical gate built in the 17th century in Delhi, India.", CityId = cities.ElementAt(5).Id },
                new() { Id = 12, Name = "Qutub Minar", Category = "Landmark", Description = "The tallest brick minaret in the world, located in Delhi, India.", CityId = cities.ElementAt(5).Id },
                new() { Id = 13, Name = "Cristo Redentor", Category = "Landmark", Description = "Art Deco statue of Jesus Christ in Rio de Janeiro, Brazil.", CityId = cities.ElementAt(6).Id },
                new() { Id = 14, Name = "Istanbul Archaeology Museums", Category = "Museum", Description = "Archaeological museum complex in Istanbul, Turkey.", CityId = cities.ElementAt(7).Id },
                new() { Id = 15, Name = "Grand Bazaar", Category = "Market", Description = "One of the largest and oldest covered markets in the world, located in Istanbul, Turkey.", CityId = cities.ElementAt(7).Id },
                new() { Id = 16, Name = "Golden Gate Bridge", Category = "Bridge", Description = "Iconic bridge that connects San Francisco to Marin County, California, USA.", CityId = cities.ElementAt(0).Id },
                new() { Id = 17, Name = "Statue of Liberty", Category = "Landmark", Description = "Symbolic statue located on Liberty Island in New York Harbor.", CityId = cities.ElementAt(0).Id },
                new() { Id = 18, Name = "The Colosseum", Category = "Landmark", Description = "An ancient amphitheater in the center of Rome, Italy.", CityId = cities.ElementAt(0).Id },
                new() { Id = 19, Name = "Acropolis of Athens", Category = "Historical Site", Description = "Ancient citadel located on a rocky outcrop above the city of Athens, Greece.", CityId = cities.ElementAt(0).Id },
                new() { Id = 20, Name = "Sydney Opera House", Category = "Landmark", Description = "Iconic structure in Sydney, Australia, known for its distinctive sail-like design.", CityId = cities.ElementAt(0).Id },
                new() { Id = 21, Name = "Petronas Towers", Category = "Skyscraper", Description = "Twin towers in Kuala Lumpur, Malaysia, once the tallest buildings in the world.", CityId = cities.ElementAt(0).Id },
                new() { Id = 22, Name = "Great Wall of China", Category = "Historical Site", Description = "A series of fortifications made of stone, brick, tamped earth, and other materials, generally built along an east-to-west line across the historical northern borders of China.",CityId = cities.ElementAt(3).Id }

            };
        }


    }
}