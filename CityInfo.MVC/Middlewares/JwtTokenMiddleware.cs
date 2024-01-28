using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CityInfo.MVC.Middlewares;

public class JwtTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly IAuthorizationService _authorizationService;

    public JwtTokenMiddleware(RequestDelegate next, IConfiguration configuration, IAuthorizationService authorizationService)
    {
        _next = next;
        _configuration = configuration;
        _authorizationService = authorizationService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Cookies["JwtToken"];
        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = 401; // Unauthorized
            return;
        }

        // Extract the user's claims and roles from the token
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var claims = jwtToken.Claims;
        var roles = claims.Where(x => x.Type == "roles").Select(x => x.Value).ToList();

        // Set the current user's identity and roles
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //identity.AddClaims(claims);
        var principal = new ClaimsPrincipal(identity);

        // Use the roles for authorization
        if (!string.IsNullOrEmpty(_configuration["Auth:Policy"]))
        {
            var authResult = await _authorizationService.AuthorizeAsync(principal, "RequireAdmin");
            if (!authResult.Succeeded)
            {
                context.Response.StatusCode = 403; // Forbidden
                return;
            }
        }

        context.User = principal;

        // Call the next middleware in the pipeline
        await _next(context);

    }
}
