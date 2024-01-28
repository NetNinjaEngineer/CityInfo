using CityInfo.MVC.DataTransferObjects.Auth;

namespace CityInfo.MVC.Services;

public interface ITokenProvider
{
    Task<string> GetTokenAsync(TokenRequestModel tokenRequestModel);
}
