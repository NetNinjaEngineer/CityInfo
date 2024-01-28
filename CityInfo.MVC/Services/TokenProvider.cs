using CityInfo.MVC.DataTransferObjects.Auth;
using Newtonsoft.Json;
using System.Text;

namespace CityInfo.MVC.Services;

public class TokenProvider : ITokenProvider
{
    private readonly HttpClient _httpClient;

    public TokenProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CityInfoApi");
    }

    public async Task<string> GetTokenAsync(TokenRequestModel tokenRequestModel)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/Auth/Login");
        var content = new StringContent(JsonConvert.SerializeObject(tokenRequestModel),
            Encoding.UTF8, "application/json");
        request.Content = content;
        var response = await _httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        var authResult = JsonConvert.DeserializeObject<AuthModel>(responseContent);

        if (response.IsSuccessStatusCode && authResult!.IsAuthenticated)
            return authResult.Token!;

        return null!;
    }
}
