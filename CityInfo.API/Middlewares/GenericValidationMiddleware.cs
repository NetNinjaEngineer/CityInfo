
using CityInfo.API.Contracts;
using Newtonsoft.Json;

namespace CityInfo.API.Middlewares;

public class GenericValidationMiddleware<T> : IMiddleware where T : class
{
    private readonly IValidationService<T> _validationService;

    public GenericValidationMiddleware(IValidationService<T> validationService)
    {
        _validationService = validationService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var entity = await DeserializeRequestBody<T>(context.Request);

        if (!_validationService.Validate(entity))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Validation failed");
        }
        else
        {
            await next(context);
        }
    }

    private async Task<T> DeserializeRequestBody<T>(HttpRequest request)
    {
        using var reader = new StreamReader(request.Body);
        var body = await reader.ReadToEndAsync();
        return JsonConvert.DeserializeObject<T>(body);
    }
}
