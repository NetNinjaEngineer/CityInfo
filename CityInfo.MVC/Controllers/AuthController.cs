using CityInfo.MVC.DataTransferObjects.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.MVC.Controllers;

[AllowAnonymous]
public class AuthController : Controller
{
    private readonly HttpClient _httpClient;
    private JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public AuthController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CityInfoApi");
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(TokenRequestModel model)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Auth/Login", model);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var authResponse = JsonConvert.DeserializeObject<AuthModel>(responseContent);
            var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(authResponse!.Token);
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
            Response.Cookies.Append("JwtToken", authResponse.Token!, new CookieOptions
            {
                HttpOnly = true
            });

            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError("", "Invalid username or password");
        }
        return View(model);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel registration)
    {
        if (ModelState.IsValid)
        {
            var returnUrl = Url.Content("~/");
            var content = new StringContent(JsonConvert.SerializeObject(registration),
                encoding: Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = null!;
            try
            {
                responseMessage = await _httpClient.PostAsync("api/Auth/Register", content);
            }
            catch
            {
                return View(registration);
            }

            var responseContent = await responseMessage
                .Content.ReadAsStringAsync();

            var authResponse = JsonConvert.DeserializeObject<AuthModel>(responseContent);

            if (authResponse!.IsAuthenticated
                && responseMessage.IsSuccessStatusCode)
            {
                var authToken = authResponse.Token;
                var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(authToken);

                var claims = tokenContent.Claims.ToList();

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    principal, new AuthenticationProperties { IsPersistent = true });

                return LocalRedirect(returnUrl);
            }

        }

        ModelState.AddModelError("", "Registration Attempt Failed. Please try again.");
        return View(registration);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new AuthenticationProperties { IsPersistent = false });

        return LocalRedirect("/");
    }
}
