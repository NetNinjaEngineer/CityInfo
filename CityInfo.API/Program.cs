using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using CityInfo.API.ActionFilters;
using CityInfo.API.Authorization.Handlers;
using CityInfo.API.Contracts;
using CityInfo.API.Data;
using CityInfo.API.Models;
using CityInfo.API.Repository;
using CityInfo.API.Repository.Implementors;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace CityInfo.API;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog();

        builder.Services.AddResponseCaching();

        builder.Services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;

            options.Filters.Add(new AuthorizeFilter());

            options.CacheProfiles.Add("240SecondsCacheProfile", new CacheProfile
            {
                Duration = 240
            });
        })
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
            .AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetailsFactory = context.HttpContext.RequestServices
                    .GetRequiredService<ProblemDetailsFactory>();

                    var validationProblemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext,
                        context.ModelState);

                    validationProblemDetails.Detail = "See the errors field for details";
                    validationProblemDetails.Instance = context.HttpContext.Request.Path;

                    var actionExecutingContext = context as ActionExecutingContext;
                    if ((context.ModelState.ErrorCount > 0)
                        && (actionExecutingContext?.ActionArguments.Count ==
                        context.ActionDescriptor.Parameters.Count))
                    {
                        validationProblemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        validationProblemDetails.Title = "One or more validation errors occurred";

                        return new UnprocessableEntityObjectResult(validationProblemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    }

                    validationProblemDetails.Status = StatusCodes.Status400BadRequest;
                    validationProblemDetails.Title = "One or more error on input occurred";
                    return new BadRequestObjectResult(validationProblemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });


        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
        var connectionString = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAutoMapper(typeof(Program).Assembly);
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<ICityRepository, CityRepository>();
        builder.Services.AddScoped<IPointOfInterestRepository, PointOfInterestRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<CityExistsFilterAttribute>();
        builder.Services.AddScoped<ValidationFilterAttribute>();
        builder.Services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();
        builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
        builder.Services.AddSingleton<IAuthorizationHandler, CityAuthorizationCrudHandler>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<SeedRolesService>();

        var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("ver"));
        });

        apiVersioningBuilder.AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:ValidIssuer"],
                    ValidAudience = builder.Configuration["JwtSettings:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
                };
            });

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("RequireAdmin", policy =>
                policy.RequireRole("Admin"))
            .AddPolicy("RequireUser", policy =>
                policy.RequireRole("User"));

        var apiVersionDescriptionProvider = builder.Services.BuildServiceProvider()
            .GetRequiredService<IApiVersionDescriptionProvider>();

        builder.Services.AddSwaggerGen(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc($"CityInfoOpenApiSpecification{description.GroupName}", new OpenApiInfo()
                {
                    Title = "CityInfoApi",
                    Version = description.ApiVersion.ToString(),
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
                });
            }

            options.AddSecurityDefinition("CityInfoApiBearerAuth", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Description = "Input a valid token to access this API"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "CityInfoApiBearerAuth"
                        }
                    },
                    new List<string>()
                }
            });

            options.DocInclusionPredicate((documentName, apiDescription) =>
            {

            });

            var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var fullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
            options.IncludeXmlComments(fullPath);
        });

        var app = builder.Build();

        var apiVersionprovider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionprovider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/CityInfoOpenApiSpecification{description.GroupName}/swagger.json",
                        $"{description.GroupName.ToUpperInvariant()}");
            });
        }
        else
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("An Unexpected fault happened, try again later.");
                });
            });

        using (var scope = app.Services.CreateScope())
        {
            var seedRolesService = scope.ServiceProvider.GetRequiredService<SeedRolesService>();
            seedRolesService.SeedRoles().Wait();
        }

        app.UseHttpsRedirection();

        app.UseResponseCaching();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
