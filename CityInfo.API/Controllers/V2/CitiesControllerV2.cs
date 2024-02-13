using Asp.Versioning;
using CityInfo.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers.V2;
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/cities")]
[ApiController]
public class CitiesControllerV2 : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;

    public CitiesControllerV2(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok("Hello, world");
    }
}
