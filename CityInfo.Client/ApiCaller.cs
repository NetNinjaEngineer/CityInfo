namespace CityInfo.Client;
internal class ApiCaller
{
    private readonly IAuthClient _authClient;
    private readonly ICitiesClient _citiesClient;

    public ApiCaller(IAuthClient authClient, ICitiesClient citiesClient)
    {
        _authClient = authClient;
        _citiesClient = citiesClient;
    }

    public async Task Login()
    {
        var requestBody = new TokenRequestModel
        {
            Email = "me5260287@gmail.com",
            Password = "Mohamed@123456"
        };
        await _authClient.LoginAsync(requestBody);

        var cities = await _citiesClient.GetCitiesAsync(null);

    }
}
