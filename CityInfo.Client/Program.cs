namespace CityInfo.Client;

internal class Program
{
    static async Task Main(string[] args)
    {
        var baseUrl = "https://localhost:7140";
        var apiCaller = new ApiCaller(new AuthClient(baseUrl, new HttpClient()),
            new CitiesClient(baseUrl, new HttpClient()));

        await apiCaller.Login();

        Console.ReadKey();
    }
}
