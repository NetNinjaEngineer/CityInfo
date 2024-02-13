using Asp.Versioning;
using CityInfo.API.Contracts;
using CityInfo.API.DataTransferObjects.Link;
using CityInfo.API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers.V2;
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/cities")]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;

    public CitiesController(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(City))]
    [HttpGet(Name = "GetCitiesAsync")]
    [HttpHead]
    public async Task<IActionResult> GetCitiesAsync(string? fields)
    {
        var cities = await unitOfWork.CityRepository.GetCitiesAsync(trackChanges: true);
        var linkedResourceToReturn = cities.Select(city =>
        {
            var cityAsDictionary = city.ShapeObject(fields!) as IDictionary<string, object>;
            var cityLinks = CreateLinksForCity(city.Id);
            cityAsDictionary?.Add("links", cityLinks);
            return cityAsDictionary;
        });

        var linkedResourceWithSelfLink = new
        {
            value = linkedResourceToReturn,
            selfLink = Url.Link("GetCitiesAsync", new { })
        };

        return Ok(linkedResourceWithSelfLink);
    }

    private List<LinkDto> CreateLinksForCity(int cityId)
    {
        var cityLinks = new List<LinkDto>();
        cityLinks.AddRange([
            new()
            {
                Href = Url.Link("GetCity", new { cityId }),
                Rel = "get_city",
                Method = "GET"
            },
            new()
            {
                Href = Url.Link("DeleteCity", new { cityId }),
                Rel = "delete_city",
                Method = "DELETE"
            },
            new()
            {
                Href = Url.Link("UpdateCity", new { cityId }),
                Rel = "update_city",
                Method = "PUT"
            },
            new()
            {
                Href = Url.Link("CreateCity", new{ }),
                Rel = "create_city",
                Method = "POST"
            }
        ]);

        return cityLinks;

    }

}
