using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CityInfo.API.Swagger;

public class ConfigureSwaggerOptions
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "CityInfoApi",
            Version = "1.0",
            Description = "Through this api you can access cities and pointsofinterest",
            Contact = new OpenApiContact()
            {
                Name = "Mohamed ElHelaly",
                Email = "me5260287@gmail.com",
                Url = new Uri("https://github.com/NetNinjaEngineer")
            },
            License = new OpenApiLicense()
            {
                Name = "MIT LICENSE",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}
