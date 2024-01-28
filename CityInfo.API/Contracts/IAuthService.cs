using CityInfo.API.DataTransferObjects.Auth;

namespace CityInfo.API.Contracts;

public interface IAuthService
{
    Task<AuthModel> RegisterAsync(RegisterModel requestModel);
    Task<AuthModel> LoginAsync(TokenRequestModel tokenRequestModel);
    Task LogoutAsync();
}
