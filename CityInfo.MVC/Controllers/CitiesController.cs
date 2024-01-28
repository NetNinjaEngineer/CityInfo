using CityInfo.MVC.Data;
using CityInfo.MVC.DataTransferObjects.City;
using CityInfo.MVC.Helpers;
using CityInfo.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace CityInfo.MVC.Controllers;

[Authorize]
public class CitiesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly HttpClient _httpClient;

    public CitiesController(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClient = httpClientFactory.CreateClient("CityInfoApi");
    }

    // Get: Cities
    public async Task<IActionResult> Index()
    {
        try
        {
            if (User.Identity!.IsAuthenticated)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/cities");
                var token = Request.Cookies["JwtToken"];
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var resposeContent = await response.Content.ReadAsStringAsync();
                    var citiesResponse = JsonConvert.DeserializeObject<CitiesResponse>(resposeContent);
                    return View(citiesResponse!.Value);
                }

                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        return BadRequest();

                    case HttpStatusCode.Unauthorized:
                        return Unauthorized();

                    case HttpStatusCode.PaymentRequired:
                        break;
                    case HttpStatusCode.Forbidden:
                        return Forbid();
                    default:
                        break;
                }


            }

            return Content("User not authenticated !!!");
        }
        catch
        {
            return Content("There was problem happened, please try again !!!");
        }

    }

    //Get: /cities/details/id
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var city = await _context.Cities.Include(c => c.PointOfInterests)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (city == null)
            return NotFound();

        return View(city);

    }

    // GET: Cities/Create
    public IActionResult Create() => View();

    //POST: Cities/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CityForCreationDto city)
    {
        var cityForCreation = new City
        {
            Name = city.Name,
            Country = city.Country,
            Latitude = city.Latitude,
            Longitude = city.Longitude,
            Population = city.Population
        };

        if (ModelState.IsValid)
        {
            _context.Add(cityForCreation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(city);
    }

    // GET: Cities/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();
        var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
        if (city is null)
            return NotFound();
        return View(city);
    }

    // PUT: Cities/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, CityForUpdateDto request)
    {
        var existCity = _context.Cities.FirstOrDefault(c => c.Id == id);
        if (existCity is null)
            return NotFound();
        if (ModelState.IsValid)
        {
            existCity.Longitude = request.Longitude;
            existCity.Latitude = request.Latitude;
            existCity.Population = request.Population;
            existCity.Country = request.Country;
            existCity.Name = request.Name;

            _context.Update(existCity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(existCity);

    }

    // GET: PointOfInterests/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pointOfInterest = await _context.Cities
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pointOfInterest == null)
        {
            return NotFound();
        }

        return View(pointOfInterest);
    }

    // POST: PointOfInterests/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var city = await _context.Cities.FindAsync(id);
        if (city != null)
        {
            _context.Cities.Remove(city);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }



}
