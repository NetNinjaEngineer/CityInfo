using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace CityInfo.API.ActionFilters;

public class SampleExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _environment;
    public SampleExceptionFilter(IHostEnvironment environment)
        => _environment = environment;
    public void OnException(ExceptionContext context)
    {
        if (!_environment.IsDevelopment())
            return;

        context.Result = new ContentResult()
        {
            Content = JsonSerializer.Serialize(context.Exception.ToString()),
            ContentType = "application/json",
            StatusCode = 500
        };
    }
}
