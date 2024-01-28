using CityInfo.API.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers.Test;

[ResponseHeader("Filter Header", "Filter value")]
[Route("api/[controller]")]
[ApiController]
public class ResponseHeaderController : ControllerBase
{
    [HttpGet]
    public IActionResult GetResult()
        => Ok("Response Headers");
}
