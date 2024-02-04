using CityInfo.API.Contracts;
using CityInfo.API.DataTransferObjects.Auth;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.API.Repository.Implementors;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public async Task<AuthModel> LoginAsync(TokenRequestModel tokenRequestModel)
    {
        var authModelToReturn = new AuthModel();
        var user = await _userManager.FindByEmailAsync(tokenRequestModel.Email!);
        if (user is null || !await _userManager.CheckPasswordAsync(user, tokenRequestModel.Password!))
        {
            authModelToReturn.Message = "Email or Password are incorrect";
            return authModelToReturn;
        }

        var securityJwtToken = await CreateJwtToken(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        authModelToReturn.IsAuthenticated = true;
        authModelToReturn.Roles = [.. userRoles];
        authModelToReturn.ExpiresOn = securityJwtToken.ValidTo;
        authModelToReturn.Token = new JwtSecurityTokenHandler().WriteToken(securityJwtToken);

        return authModelToReturn;

    }


    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<AuthModel> RegisterAsync(RegisterModel requestModel)
    {
        if (await _userManager.FindByEmailAsync(requestModel.Email!) is not null)
            return new AuthModel { Message = "Email is already registered!" };

        if (await _userManager.FindByNameAsync(requestModel.Username!) is not null)
            return new AuthModel { Message = "Username is already registered !" };

        var user = new AppUser
        {
            UserName = requestModel.Username,
            Email = requestModel.Email,
            FirstName = requestModel.FirstName,
            LastName = requestModel.LastName
        };

        var result = await _userManager.CreateAsync(user, requestModel.Password!);

        if (!result.Succeeded)
        {
            var errors = string.Empty;

            foreach (var error in result.Errors)
            {
                errors += error.Description;
            }

            return new AuthModel { Message = errors };
        }

        await _userManager.AddToRoleAsync(user, "User");

        var jwtSecurityToken = await CreateJwtToken(user);

        return new AuthModel
        {
            Email = user.Email,
            IsAuthenticated = true,
            ExpiresOn = jwtSecurityToken.ValidTo,
            Roles = ["User"],
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            UserName = user.UserName
        };
    }

    private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach (var role in roles)
            roleClaims.Add(new Claim("roles", role));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("uid", user.Id)
        }
            .Union(userClaims)
            .Union(roleClaims);

        var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:ValidIssuer"],
            audience: _configuration["JwtSettings:ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signingCredentials
        );

        return jwtSecurityToken;
    }
}